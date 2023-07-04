CREATE DATABASE Practical12_Test2;
GO

USE Practical12_Test2;
GO

CREATE TABLE Employee (
	[Id] INT IDENTITY(1, 1) PRIMARY KEY,
	[First Name] VARCHAR(50) NOT NULL,
	[Middle Name] VARCHAR(50),
	[Last Name] VARCHAR(50) NOT NULL,
	[DOB] DATE NOT NULL,
	[Mobile Number] VARCHAR(10) NOT NULL,
	[Address] VARCHAR(100),
	[Salary] DECIMAL NOT NULL
);
GO

INSERT INTO Employee VALUES 
('Jay', 'aaa', 'Koshti', '1999-10-24', '123456789', 'address1', 50000),
('Ajay', 'bbb', 'Koshti', '1920-11-23', '123123123', 'address2', 40000),
('Sanjay', 'ccc', 'Koshti', '2001-9-1', '123412341', 'address3', 30000),
('Preety', 'ddd', 'Koshti', '2001-10-24', '121212121', 'address4', 10000);
GO

SELECT * FROM [dbo].Employee;
GO

--TRUNCATE TABLE [dbo].Employee
--GO

