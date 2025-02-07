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

--Procedure and functions
CREATE PROCEDURE GetTransactionLogs
AS
BEGIN
    SELECT LogID, TableName, Operation, ChangeDate, Details, UserID 
    FROM TransactionLogs;
END
GO

CREATE PROCEDURE GetStudentById
    @Identifier NVARCHAR(50)
AS
BEGIN
    SELECT StudentID, FullName, Email, Department, Year, Password 
    FROM Students 
    WHERE StudentID = @Identifier OR Email = @Identifier;
END
GO

CREATE PROCEDURE ValidateCredentials
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT StudentID 
    FROM Students 
    WHERE (Email = @Username OR StudentID = @Username) 
    AND Password = @Password;
END
GO

CREATE PROCEDURE GetRequestById
    @RequestID INT
AS
BEGIN
    SELECT RequestID, StudentID, RequestDate, Status, Remark 
    FROM IDRequests 
    WHERE RequestID = @RequestID;
END
GO

CREATE PROCEDURE GetRequestsByStudentId
    @StudentID NVARCHAR(50)
AS
BEGIN
    SELECT RequestID, StudentID, RequestDate, Status, Remark 
    FROM IDRequests 
    WHERE StudentID = @StudentID;
END
GO

CREATE PROCEDURE AddRequest
    @StudentID NVARCHAR(50),
    @Status NVARCHAR(50),
    @RequestID INT OUTPUT
AS
BEGIN
    INSERT INTO IDRequests (StudentID, Status) 
    VALUES (@StudentID, @Status);

    SET @RequestID = SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE UpdateRequestStatus
    @RequestID INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE IDRequests 
    SET Status = @Status 
    WHERE RequestID = @RequestID;
END
GO

CREATE PROCEDURE GetRequestsWithAppointmentByStudentId
    @StudentID NVARCHAR(50)
AS
BEGIN
    SELECT 
        S.StudentID,
        IR.RequestDate,
        A.AppointmentDate,
        IR.Status
    FROM 
        IDRequests IR
    JOIN 
        Appointments A ON IR.RequestID = A.RequestID
    JOIN 
        Students S ON IR.StudentID = S.StudentID
    WHERE 
        IR.StudentID = @StudentID;
END
GO
-- GetDocumentById Procedure
CREATE PROCEDURE GetDocumentById
    @DocumentID INT
AS
BEGIN
    SELECT DocumentID, RequestID, DocumentPath, UploadDate 
    FROM Documents 
    WHERE DocumentID = @DocumentID;
END
GO

-- GetDocumentsByRequestId Procedure
CREATE PROCEDURE GetDocumentsByRequestId
    @RequestID INT
AS
BEGIN
    SELECT DocumentID, RequestID, DocumentPath, UploadDate 
    FROM Documents 
    WHERE RequestID = @RequestID;
END
GO

-- AddDocument Procedure
CREATE PROCEDURE AddDocument
    @RequestID INT,
    @DocumentPath NVARCHAR(255),
    @DocumentID INT OUTPUT
AS
BEGIN
    INSERT INTO Documents (RequestID, DocumentPath) 
    VALUES (@RequestID, @DocumentPath);

    SET @DocumentID = SCOPE_IDENTITY();
END
GO
-- GetAppointmentById Procedure
CREATE PROCEDURE GetAppointmentById
    @AppointmentID INT
AS
BEGIN
    SELECT AppointmentID, RequestID, AppointmentDate 
    FROM Appointments 
    WHERE AppointmentID = @AppointmentID;
END
GO

-- GetAppointmentsByRequestId Procedure
CREATE PROCEDURE GetAppointmentsByRequestId
    @RequestID INT
AS
BEGIN
    SELECT AppointmentID, RequestID, AppointmentDate 
    FROM Appointments 
    WHERE RequestID = @RequestID;
END
GO

-- AddAppointment Procedure
CREATE PROCEDURE AddAppointment
    @RequestID INT,
    @AppointmentDate DATETIME,
    @AppointmentID INT OUTPUT
AS
BEGIN
    INSERT INTO Appointments (RequestID, AppointmentDate) 
    VALUES (@RequestID, @AppointmentDate);

    SET @AppointmentID = SCOPE_IDENTITY();
END
GO

-- UpdateAppointmentDate Procedure
CREATE PROCEDURE UpdateAppointmentDate
    @AppointmentID INT,
    @NewDate DATETIME
AS
BEGIN
    UPDATE Appointments 
    SET AppointmentDate = @NewDate 
    WHERE AppointmentID = @AppointmentID;
END
GO

-- GetAllPendingStudents Procedure
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
END
GO

-- GetAllCompletedStudents Procedure
CREATE PROCEDURE GetAllCompletedStudents
AS
BEGIN
    SELECT r.RequestID, s.StudentID, s.FullName, a.AppointmentDate, r.Status, d.DocumentPath, r.Remark
    FROM IDRequests r
    INNER JOIN Students s ON r.StudentID = s.StudentID
    LEFT JOIN Appointments a ON r.RequestID = a.RequestID
    LEFT JOIN Documents d ON r.RequestID = d.RequestID
    WHERE r.Status != 'Pending'
    ORDER BY r.RequestDate DESC;
END
GO

-- AcceptRequest Procedure
CREATE PROCEDURE AcceptRequest
    @RequestID INT
AS
BEGIN
    UPDATE IDRequests
    SET Status = 'Approved'
    WHERE RequestID = @RequestID;
END
GO

-- DenyRequest Procedure
CREATE PROCEDURE DenyRequest
    @RequestID INT
AS
BEGIN
    UPDATE IDRequests
    SET Status = 'Rejected'
    WHERE RequestID = @RequestID;
END
GO
CREATE PROCEDURE CheckPendingRequest
    @StudentID INT
AS
BEGIN
    -- Declare a variable to store the count of pending requests
    DECLARE @PendingCount INT;

    -- Check the count of pending requests for the student
    SELECT @PendingCount = COUNT(*)
    FROM IDRequests
    WHERE StudentID = @StudentID AND Status = 'Pending';

    -- Return the count of pending requests
    SELECT @PendingCount AS PendingCount;
END;
GO