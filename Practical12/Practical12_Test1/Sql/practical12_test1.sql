CREATE DATABASE Practical12_Test1;
GO

USE Practical12_Test1;
GO

CREATE TABLE Employee (
	[Id] INT IDENTITY(1, 1) PRIMARY KEY,
	[First Name] VARCHAR(50) NOT NULL,
	[Middle Name] VARCHAR(50),
	[Last Name] VARCHAR(50) NOT NULL,
	[DOB] DATE NOT NULL,
	[Mobile Number] VARCHAR(10) NOT NULL,
	[Address] VARCHAR(100),
);
GO

INSERT INTO Employee VALUES 
('Jay', 'aaa', 'Koshti', '2001-10-24', '123456789', 'address1'),
('Ajay', 'bbb', 'Koshti', '2001-11-23', '123123123', 'address2'),
('Sanjay', 'ccc', 'Koshti', '2001-9-1', '123412341', 'address3'),
('Preety', 'ddd', 'Koshti', '2001-10-24', '121212121', 'address4');
GO

SELECT * FROM [dbo].Employee;
GO

--TRUNCATE TABLE [dbo].Employee
--GO