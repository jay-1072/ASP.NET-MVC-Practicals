CREATE DATABASE Practical12_Test3;
GO

USE Practical12_Test3;
GO

CREATE TABLE Designation (
	[Id] INT IDENTITY(1, 1) PRIMARY KEY,
	[Designation] VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Employee (
	[Id] INT IDENTITY(1, 1) PRIMARY KEY,
	[First Name] VARCHAR(50) NOT NULL,
	[Middle Name] VARCHAR(50),
	[Last Name] VARCHAR(50) NOT NULL,	
	[DOB] DATE NOT NULL,
	[Mobile Number] VARCHAR(10) NOT NULL,
	[Address] VARCHAR(100),
	[Salary] DECIMAL NOT NULL,
	[DesignationId] INT FOREIGN KEY REFERENCES DESIGNATION(Id)
);
GO

INSERT INTO [dbo].Designation VALUES ('MANAGER'), ('SALESMAN'), ('CLERK'), ('ANALYST');
GO

SELECT * FROM [dbo].Designation;
GO

INSERT INTO [dbo].Employee
	([First Name], [Middle Name], [Last Name], [DOB], [Mobile Number], [Address], [Salary], [DesignationId])
	VALUES 
	('Jay', 'Chetan', 'Koshti', '2001-10-24', '9510991230', 'address1', 50000, 4),
	('Arpit', 'Ramesh', 'Gohel', '2002-06-28', '1239912301', 'address2', 90000, 1),
	('Abhi', NULL, 'Dashadiya', '2001-09-25', '9991299121', 'address3', 60000, 2),
	('Parth', 'Vivek', 'Patel', '1999-10-21', '9898912991', NULL, 40000, 3),
	('Preety', 'Bhavin', 'Patel', '1998-09-15', '1212123123', NULL, 60000, 1);
GO

SELECT * FROM [dbo].Employee;
GO

-- Write a query to count the number of records by designation name

SELECT d.Designation,  [Employee Count] = COUNT(e.Id)
FROM [dbo].Employee e
INNER JOIN [dbo].Designation d ON e.[DesignationId] = d.[Id]
GROUP BY d.Designation;

-- Write a query to display First Name, Middle Name, Last Name & Designation name

SELECT e.[First Name], e.[Middle Name], e.[Last Name], d.[Designation]
FROM [dbo].Employee e
INNER JOIN [dbo].Designation d ON e.[DesignationId] = d.[Id];

-- Create a database view that outputs Employee Id, First Name, Middle Name, Last Name, 
-- Designation, DOB, Mobile Number, Address & Salary

CREATE OR ALTER VIEW employee_info
AS
SELECT 
	e.[Id],
	e.[First Name],
	e.[Middle Name],
	e.[Last Name],
	d.[Designation],
	e.[DOB],
	e.[Mobile Number],
	e.[Address],
	e.[Salary]
FROM 
	[dbo].Employee e
	INNER JOIN [dbo].Designation d ON e.DesignationId = d.Id;
GO

-- Create a stored procedure to insert data into the Designation table with required parameters

CREATE OR ALTER PROC uspInsertDesignation (@DESIGNATION VARCHAR(50)) 
AS
BEGIN
	INSERT INTO [dbo].Designation([Designation]) VALUES (@DESIGNATION)
END;
GO

-- Create a stored procedure to insert data into the Employee table with required parameters

CREATE OR ALTER PROC uspInsertEmployee (@FName VARCHAR(50), @MName VARCHAR(50), @LName VARCHAR(50), @dob DATE, @MNumber VARCHAR(10), @Address VARCHAR(100), @Salary DECIMAL, @DesignationId INT) 
AS
BEGIN
	INSERT INTO [dbo].Employee ([First Name], [Middle Name], [Last Name], [DOB], [Mobile Number], [Address], [Salary], [DesignationId]) 
	VALUES 
	(@FName, @MName, @LName, @dob, @MNumber, @Address, @Salary, @DesignationId)
END;
GO

-- Write a query that displays only those designation names that have more than 1 employee

SELECT d.Designation,  [Employee Count] = COUNT(e.Id)
FROM [dbo].Employee e
INNER JOIN [dbo].Designation d ON e.[DesignationId] = d.[Id]
GROUP BY d.Designation HAVING COUNT(e.[Id]) > 1;


--  Create a stored procedure that returns a list of employees with columns Employee Id, First Name, Middle Name, Last Name,
-- Designation, DOB, Mobile Number, Address & Salary (records should be ordered by DOB)

CREATE OR ALTER PROC uspGetEmployeeOrderByDOB 
AS
BEGIN
	SELECT e.[Id], e.[First Name], e.[Middle Name], e.[Last Name], d.[Designation], e.[DOB], e.[Mobile Number], e.[Address], e.[Salary] 
	FROM
	[dbo].Employee e
	INNER JOIN [dbo].Designation d ON e.DesignationId = d.Id
	ORDER BY e.[DOB]
END;
GO

-- Create a stored procedure that return a list of employees by designation id (Input) with columns 
-- Employee Id, First Name, Middle Name, Last Name, DOB, Mobile Number, Address & Salary (records should be ordered by First Name)

CREATE OR ALTER PROC uspGetEmployeeByDesignationId (@DesignationId INT) 
AS
BEGIN
	SELECT e.[Id], e.[First Name], e.[Middle Name], e.[Last Name], e.[DOB], e.[Mobile Number], e.[Address], e.[Salary] 
	FROM
	[dbo].Employee e
	INNER JOIN [dbo].Designation d ON e.DesignationId = d.Id
	WHERE d.[Id] = @DesignationId
	ORDER BY e.[First Name]
END;
GO

-- Create Non-Clustered index on the DesignationId column of the Employee table

CREATE NONCLUSTERED INDEX ix_employee_designationId
ON [dbo].Employee([DesignationId]);
GO

-- Write a query to find the employee having maximum salary

SELECT * FROM (
	SELECT *, [my_dense] = DENSE_RANK() OVER (ORDER BY [Salary] DESC) FROM [dbo].Employee	
) t WHERE T.[my_dense] = 1;



--SELECT * FROM [employee_info];
--GO

--EXEC uspInsertDesignation @DESIGNATION = 'LEAD ENGINEER';
--GO

--EXEC uspInsertEmployee @FName='Sapna', @MName=NULL, @LName='Patel', @dob='2001-10-25', @MNumber='1112223331', @Address = 'address6', @Salary = 50000, @DesignationId=5; 
--GO

--SELECT * FROM Employee
--GO

--EXEC [uspGetEmployeeOrderByDOB];
--GO

--EXEC uspGetEmployeeByDesignationId @DesignationId = 2;
--GO