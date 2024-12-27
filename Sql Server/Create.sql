CREATE DATABASE StudentIDCardSystem;
GO

USE StudentIDCardSystem;
GO

CREATE TABLE Students (
    StudentID VARCHAR(20) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Phone VARCHAR(20)
)
GO

CREATE TABLE Requests (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID VARCHAR(20) FOREIGN KEY REFERENCES Students(StudentID),
    SubmissionDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(20) DEFAULT 'Pending',
    Reason NVARCHAR(500),
    AdminComments NVARCHAR(500),
    CONSTRAINT CK_Status CHECK (Status IN ('Pending', 'Approved', 'Rejected', 'Completed'))
)
GO

CREATE TABLE Documents (
    DocumentID INT IDENTITY(1,1) PRIMARY KEY,
    RequestID INT FOREIGN KEY REFERENCES Requests(RequestID),
    FilePath NVARCHAR(500) NOT NULL,
    UploadDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID VARCHAR(20) FOREIGN KEY REFERENCES Students(StudentID),
    RequestID INT FOREIGN KEY REFERENCES Requests(RequestID),
    AppointmentDate DATE NOT NULL,
    TimeSlot TIME NOT NULL,
    Status VARCHAR(20) DEFAULT 'Scheduled',
    CONSTRAINT CK_AppointmentStatus CHECK (Status IN ('Scheduled', 'Completed', 'Cancelled'))
)
GO

INSERT INTO Students
VALUES ('PI1531', 'Nahom Ketema', 'NahomKetema@gmail.com', '0920741800', 'Nahom2003#')

