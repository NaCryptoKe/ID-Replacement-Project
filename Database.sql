-- 1️⃣ CREATE DATABASE
CREATE DATABASE IDRepSysstem;
GO

USE IDRepSysstem;
GO

-- 2️⃣ TABLE CREATION (Order Ensured for Foreign Keys)

CREATE TABLE Students (
    StudentID VARCHAR(10) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Department NVARCHAR(50),
    Year INT,
    Email NVARCHAR(100),
    Password NVARCHAR(25) NOT NULL
);

CREATE TABLE IDRequests (
    RequestID INT IDENTITY PRIMARY KEY,
    StudentID VARCHAR(10) FOREIGN KEY REFERENCES Students(StudentID),
    RequestDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
    Remark NVARCHAR(MAX)
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

-- 3️⃣ STORED PROCEDURES (CRUD & Utility Functions First)

-- Insert a new ID request
CREATE PROCEDURE InsertIDRequest
    @StudentID VARCHAR(10),
    @Status NVARCHAR(20),
    @Remark NVARCHAR(MAX),
    @RequestID INT OUTPUT
AS
BEGIN
    INSERT INTO IDRequests (StudentID, Status, Remark)
    VALUES (@StudentID, @Status, @Remark);

    SET @RequestID = SCOPE_IDENTITY();
END;
GO

-- Insert a document linked to a specific ID request
CREATE PROCEDURE InsertDocument
    @RequestID INT,
    @DocumentPath NVARCHAR(255)
AS
BEGIN
    INSERT INTO Documents (RequestID, DocumentPath)
    VALUES (@RequestID, @DocumentPath);

    INSERT INTO TransactionLogs (TableName, Operation, Details, UserID)
    VALUES ('Documents', 'INSERT', 'Inserted document for RequestID ' + CAST(@RequestID AS NVARCHAR), 'System');
END;
GO

-- Insert an appointment linked to a specific ID request
CREATE PROCEDURE InsertAppointment
    @RequestID INT,
    @AppointmentDate DATETIME
AS
BEGIN
    INSERT INTO Appointments (RequestID, AppointmentDate)
    VALUES (@RequestID, @AppointmentDate);

    INSERT INTO TransactionLogs (TableName, Operation, Details, UserID)
    VALUES ('Appointments', 'INSERT', 'Inserted appointment for RequestID ' + CAST(@RequestID AS NVARCHAR), 'System');
END;
GO

-- Create the complete ID request process
CREATE PROCEDURE CreateIDRequestProcess
    @StudentID VARCHAR(10),
    @Status NVARCHAR(20),
    @Remark NVARCHAR(MAX),
    @DocumentPath NVARCHAR(255),
    @AppointmentDate DATETIME
AS
BEGIN
    DECLARE @RequestID INT;

    EXEC InsertIDRequest @StudentID, @Status, @Remark, @RequestID OUTPUT;
    EXEC InsertDocument @RequestID, @DocumentPath;
    EXEC InsertAppointment @RequestID, @AppointmentDate;

    INSERT INTO TransactionLogs (TableName, Operation, Details, UserID)
    VALUES ('IDRequests', 'INSERT', 'Processed ID Request ' + CAST(@RequestID AS NVARCHAR), 'System');
END;
GO

-- Retrieve logs for overbooked dates
CREATE PROCEDURE GetOverbookedDates
AS
BEGIN
    SELECT CAST(AppointmentDate AS DATE) AS AppointmentDate
    FROM Appointments
    GROUP BY CAST(AppointmentDate AS DATE)
    HAVING COUNT(*) >= 5;
END;
GO

-- Retrieve all transaction logs
CREATE PROCEDURE GetTransactionLogs
AS
BEGIN
    SELECT * FROM TransactionLogs;
END;
GO

-- Get a student by ID or email
CREATE PROCEDURE GetStudentById
    @Identifier NVARCHAR(50)
AS
BEGIN
    SELECT * FROM Students
    WHERE StudentID = @Identifier OR Email = @Identifier;
END;
GO

-- Validate login credentials
CREATE PROCEDURE ValidateCredentials
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT StudentID FROM Students
    WHERE (Email = @Username OR StudentID = @Username) 
    AND Password = @Password;
END;
GO

-- Get a request by ID
CREATE PROCEDURE GetRequestById
    @RequestID INT
AS
BEGIN
    SELECT * FROM IDRequests WHERE RequestID = @RequestID;
END;
GO

-- Get all requests for a specific student
CREATE PROCEDURE GetRequestsByStudentId
    @StudentID NVARCHAR(50)
AS
BEGIN
    SELECT * FROM IDRequests WHERE StudentID = @StudentID;
END;
GO

-- Approve or reject requests
CREATE PROCEDURE UpdateRequestStatus
    @RequestID INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE IDRequests SET Status = @Status WHERE RequestID = @RequestID;
END;
GO

-- Retrieve pending students
CREATE PROCEDURE GetAllPendingStudents
AS
BEGIN
    SELECT r.RequestID, s.StudentID, s.FullName, a.AppointmentDate, r.Status, d.DocumentPath, r.Remark
    FROM IDRequests r
    INNER JOIN Students s ON r.StudentID = s.StudentID
    LEFT JOIN Appointments a ON r.RequestID = a.RequestID
    LEFT JOIN Documents d ON r.RequestID = d.RequestID
    WHERE r.Status = 'Pending'
    ORDER BY a.AppointmentDate;
END;
GO

-- 4️⃣ TRIGGERS (Last Since They Depend on Data Manipulation)

CREATE TRIGGER LogTransaction_Students
ON Students
FOR INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @Operation NVARCHAR(50), @Details NVARCHAR(MAX), @UserID NVARCHAR(50) = 'System';

    IF EXISTS (SELECT * FROM inserted)
        SET @Operation = 'INSERT';
    IF EXISTS (SELECT * FROM deleted)
        SET @Operation = 'DELETE';
    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @Operation = 'UPDATE';

    SET @Details = 'Affected record with StudentID: ' + CAST((SELECT TOP 1 StudentID FROM inserted) AS NVARCHAR);

    INSERT INTO TransactionLogs (TableName, Operation, ChangeDate, Details, UserID)
    VALUES ('Students', @Operation, GETDATE(), @Details, @UserID);
END;
GO

-- Log changes in IDRequests
CREATE TRIGGER LogTransaction_IDRequests
ON IDRequests
FOR INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @Operation NVARCHAR(50), @Details NVARCHAR(MAX), @UserID NVARCHAR(50) = 'System';

    IF EXISTS (SELECT * FROM inserted)
        SET @Operation = 'INSERT';
    IF EXISTS (SELECT * FROM deleted)
        SET @Operation = 'DELETE';
    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @Operation = 'UPDATE';

    SET @Details = 'Affected request with ID: ' + CAST((SELECT TOP 1 RequestID FROM inserted) AS NVARCHAR);

    INSERT INTO TransactionLogs (TableName, Operation, ChangeDate, Details, UserID)
    VALUES ('IDRequests', @Operation, GETDATE(), @Details, @UserID);
END;
GO
