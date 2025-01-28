-- 1. Create Database
CREATE DATABASE IDRepSysstem;
GO

USE IDRepSysstem;
GO

-- 2. Tables
CREATE TABLE Students (
    StudentID VARCHAR(10) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Department NVARCHAR(50),
    Year INT,
    Email NVARCHAR(100),
    Password NVARCHAR(25) DEFAULT 'PASSWORD123'
);

CREATE TABLE IDRequests (
    RequestID INT IDENTITY PRIMARY KEY,
    StudentID VARCHAR(10) FOREIGN KEY REFERENCES Students(StudentID), -- Fixed to match Students.StudentID
    RequestDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Approved', 'Rejected', 'Completed')),
    NotificationSent BIT DEFAULT 0
);

CREATE TABLE Documents (
    DocumentID INT IDENTITY PRIMARY KEY,
    RequestID INT FOREIGN KEY REFERENCES IDRequests(RequestID),
    DocumentPath NVARCHAR(255) NOT NULL,
    UploadDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Appointments (
    AppointmentID INT IDENTITY PRIMARY KEY,
    RequestID INT FOREIGN KEY REFERENCES IDRequests(RequestID),
    AppointmentDate DATETIME NOT NULL
);

CREATE TABLE TransactionLogs (
    LogID INT IDENTITY PRIMARY KEY,
    TableName NVARCHAR(50),
    Operation NVARCHAR(50),
    ChangeDate DATETIME DEFAULT GETDATE(),
    Details NVARCHAR(MAX),
    UserID NVARCHAR(50)
);
GO

-- 3. Function (Created BEFORE stored procedures that reference it)
CREATE FUNCTION dbo.GetNextAvailableAppointment()
RETURNS DATETIME
AS
BEGIN
    DECLARE @NextSlot DATETIME = GETDATE();
    DECLARE @MaxAppointments INT = 5;
    WHILE 1 = 1
    BEGIN
        -- Skip weekends (1=Sunday, 7=Saturday)
        IF DATEPART(WEEKDAY, @NextSlot) IN (1, 7)
        BEGIN
            SET @NextSlot = DATEADD(DAY, 1, @NextSlot);
            CONTINUE;
        END
        -- Check available slots for the day
        IF (SELECT COUNT(*) FROM Appointments 
            WHERE CAST(AppointmentDate AS DATE) = CAST(@NextSlot AS DATE)) < @MaxAppointments
        BEGIN
            BREAK;
        END
        SET @NextSlot = DATEADD(DAY, 1, @NextSlot);
    END
    RETURN @NextSlot;
END;
GO

-- 4. Stored Procedures
CREATE PROCEDURE AddIDRequest
    @StudentID VARCHAR(10),
    @Status NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Students WHERE StudentID = @StudentID)
        BEGIN
            RAISERROR ('StudentID does not exist.', 16, 1);
            RETURN;
        END
        INSERT INTO IDRequests (StudentID, Status) VALUES (@StudentID, @Status);
        SELECT SCOPE_IDENTITY() AS NewRequestID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE AddDocument
    @RequestID INT,
    @DocumentPath NVARCHAR(255)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM IDRequests WHERE RequestID = @RequestID)
        BEGIN
            RAISERROR ('RequestID does not exist.', 16, 1);
            RETURN;
        END
        INSERT INTO Documents (RequestID, DocumentPath) VALUES (@RequestID, @DocumentPath);
        UPDATE IDRequests SET Status = 'Approved' WHERE RequestID = @RequestID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ScheduleAppointment
    @RequestID INT,
    @AppointmentDate DATETIME = NULL
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM IDRequests WHERE RequestID = @RequestID)
        BEGIN
            RAISERROR ('RequestID does not exist.', 16, 1);
            RETURN;
        END
        IF @AppointmentDate IS NULL
        BEGIN
            SET @AppointmentDate = dbo.GetNextAvailableAppointment();
        END
        IF (SELECT COUNT(*) FROM Appointments 
            WHERE CAST(AppointmentDate AS DATE) = CAST(@AppointmentDate AS DATE)) >= 5
        BEGIN
            SET @AppointmentDate = dbo.GetNextAvailableAppointment();
        END
        INSERT INTO Appointments (RequestID, AppointmentDate) VALUES (@RequestID, @AppointmentDate);
        UPDATE IDRequests SET Status = 'Completed' WHERE RequestID = @RequestID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

-- 5. Triggers
CREATE TRIGGER LogInsert
ON IDRequests
AFTER INSERT
AS
BEGIN
    INSERT INTO TransactionLogs (TableName, Operation, Details, UserID)
    SELECT 'IDRequests', 'INSERT', 
           CONCAT('RequestID: ', INSERTED.RequestID, ', Status: ', INSERTED.Status), 
           SYSTEM_USER
    FROM INSERTED;
END;
GO

CREATE TRIGGER LogUpdateDelete
ON IDRequests
AFTER UPDATE, DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM DELETED)
    BEGIN
        INSERT INTO TransactionLogs (TableName, Operation, Details, UserID)
        SELECT 'IDRequests', 'DELETE', 
               CONCAT('Deleted RequestID: ', DELETED.RequestID), 
               SYSTEM_USER
        FROM DELETED;
    END
    IF EXISTS (SELECT 1 FROM INSERTED)
    BEGIN
        INSERT INTO TransactionLogs (TableName, Operation, Details, UserID)
        SELECT 'IDRequests', 'UPDATE', 
               CONCAT('Updated RequestID: ', INSERTED.RequestID, ', New Status: ', INSERTED.Status), 
               SYSTEM_USER
        FROM INSERTED;
    END
END;
GO

CREATE TRIGGER NotifyOnCompletion
ON IDRequests
AFTER UPDATE
AS
BEGIN
    UPDATE IDRequests
    SET NotificationSent = 0
    WHERE Status = 'Completed' AND NotificationSent = 0;
END;
GO

CREATE TRIGGER PreventDuplicateLogs
ON TransactionLogs
AFTER INSERT
AS
BEGIN
    DELETE FROM TransactionLogs
    WHERE LogID IN (
        SELECT LogID
        FROM (
            SELECT LogID, 
                   ROW_NUMBER() OVER (PARTITION BY TableName, Details, Operation ORDER BY ChangeDate DESC) AS RowNum
            FROM TransactionLogs
        ) Temp
        WHERE RowNum > 1
    );
END;
GO

-- 6. Notification Procedures
CREATE PROCEDURE GetPendingNotifications
AS
BEGIN
    SELECT
        R.RequestID,
        S.FullName,
        S.Email
    FROM IDRequests R
    JOIN Students S ON R.StudentID = S.StudentID
    WHERE R.Status = 'Completed' AND R.NotificationSent = 0;
END;
GO

CREATE PROCEDURE MarkNotificationSent
    @RequestID INT
AS
BEGIN
    UPDATE IDRequests
    SET NotificationSent = 1
    WHERE RequestID = @RequestID;
END;
GO

-- 7. Test Data (Adjusted StudentID values to strings)
INSERT INTO Students (StudentID, FullName, Department, Year, Email)
VALUES 
('1', 'John Doe', 'Computer Science', 3, 'john.doe@gmail.com'),
('2', 'Jane Smith', 'Computer Science', 2, 'jane.smith@gmail.com'),
('3', 'George Smith', 'Engineering', 2, 'george.smith@gmail.com'),
('4', 'Gordon Smith', 'Engineering', 2, 'gordan.smith@yahoo.com'),
('5', 'Jon Doo', 'Computer Science', 3, 'jon.doe@hotmail.com'),
('6', 'Lemar Odem', 'Engineering', 1, 'odem.lemar@yahoo.com');
GO

-- Test Workflow
EXEC AddIDRequest @StudentID = '6', @Status = 'Pending'; -- Use string for StudentID
EXEC AddDocument @RequestID = 1, @DocumentPath = 'c:/file/document.pdf'; -- RequestID 1 is auto-generated

DECLARE @NextSlot DATETIME = dbo.GetNextAvailableAppointment();
EXEC ScheduleAppointment @RequestID = 1, @AppointmentDate = @NextSlot;

-- Verify results
SELECT * FROM Students;
SELECT * FROM Documents;
SELECT * FROM IDRequests;
SELECT * FROM Appointments;
SELECT * FROM TransactionLogs;
EXEC GetPendingNotifications;
EXEC MarkNotificationSent @RequestID = 1; -- Corrected RequestID
GO