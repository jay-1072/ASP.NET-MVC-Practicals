using Practical12_Test3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Practical12_Test3.Data
{
    public class Connection
    {
        SqlConnection conn = null;
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        private List<DesignationModel> _designations = new List<DesignationModel>();
        private List<EmployeeModel> _employees = new List<EmployeeModel>();

        public List<DesignationModel> FetchDesignations()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM [dbo].Designation;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DesignationModel designation = new DesignationModel()
                    {
                        Id = (int)reader["Id"],
                        Designation = (string)reader["Designation"]
                    };

                    _designations.Add(designation);
                }
                return _designations;
            }
        }

        public int AddDesignation(DesignationModel designation)
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO [dbo].Designation ([Designation]) VALUES (@designation);";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@designation", designation.Designation);
                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }

        public List<EmployeeModel> FetchEmployees()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM [dbo].Employee;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var temp = reader["Middle Name"]?.ToString();

                    EmployeeModel employee = new EmployeeModel()
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["First Name"],
                        MiddleName = string.IsNullOrEmpty(reader["Middle Name"]?.ToString()) ? "NA" : reader["Middle Name"]?.ToString(),
                        LastName = (string)reader["Last Name"],
                        DOB = (DateTime)reader["DOB"],
                        MobileNumber = (string)reader["Mobile Number"],
                        Address = string.IsNullOrEmpty(reader["Address"]?.ToString()) ? "NA" : reader["Address"].ToString(),
                        Salary = (decimal)reader["Salary"]
                    };

                    _employees.Add(employee);
                }
                return _employees;
            }
        }

        public List<EmployeeWithDesignationModel> EmployeeDataWithDesignation()
        {
            List<EmployeeWithDesignationModel> employeeWithDesignationModels = new List<EmployeeWithDesignationModel>();

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT e.[First Name], e.[Middle Name], e.[Last Name], d.[Designation] FROM[dbo].Employee e
                                INNER JOIN[dbo].Designation d ON e.[DesignationId] = d.[Id];";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeWithDesignationModel employee = new EmployeeWithDesignationModel()
                    {
                        FirstName = (string)reader["First Name"],
                        MiddleName =  String.IsNullOrEmpty(reader["Middle Name"]?.ToString()) ? "NA" : reader["Middle Name"].ToString(),
                        LastName = (string)reader["Last Name"],
                        Designation = (string)reader["Designation"]
                    };

                    employeeWithDesignationModels.Add(employee);
                }
                return employeeWithDesignationModels;
            }
        }

        public List<EmployeeWithDesignationModel> DesignationWithMoreThan1Employee()
        {
            List<EmployeeWithDesignationModel> designationHavingMoreThan1Employee = new List<EmployeeWithDesignationModel>();

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT d.Designation,  [Employee Count] = COUNT(e.Id)
                                FROM [dbo].Employee e
                                INNER JOIN [dbo].Designation d ON e.[DesignationId] = d.[Id]
                                GROUP BY d.Designation HAVING COUNT(e.[Id]) > 1;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeWithDesignationModel result = new EmployeeWithDesignationModel()
                    {
                        Designation = (string)reader["Designation"],
                        Count = (int)reader["Employee Count"]
                    };

                    designationHavingMoreThan1Employee.Add(result);
                }
                return designationHavingMoreThan1Employee;
            }
        }

        public bool CreateStoreProcedureToAddDesignation()
        {
            bool isStoreProcedureAdded = false;

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"CREATE OR ALTER PROC uspInsertDesignation (@DESIGNATION VARCHAR(50)) 
                               AS
                               BEGIN
	                              INSERT INTO [dbo].Designation([Designation]) VALUES (@DESIGNATION)
                               END;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    isStoreProcedureAdded = true;
                }
            }
            return isStoreProcedureAdded;
        }

        public List<EmployeeWithDesignationModel> NumberOfRecordByDesignation()
        {
            List<EmployeeWithDesignationModel> numberOfRecordByDesignationName = new List<EmployeeWithDesignationModel>();

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT d.Designation, [Employee Count] = COUNT(e.Id)
                             FROM [dbo].Employee e
                             INNER JOIN [dbo].Designation d ON e.[DesignationId] = d.[Id]
                             GROUP BY d.Designation;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeWithDesignationModel result = new EmployeeWithDesignationModel()
                    {
                        Designation = (string)reader["Designation"],
                        Count = (int)reader["Employee Count"]
                    };

                    numberOfRecordByDesignationName.Add(result);
                }
                return numberOfRecordByDesignationName;
            }
        }

        public List<EmployeeModel> EmployeeHavingMaxSalary()
        {
            List<EmployeeModel> employeeHavingMaxSalary = new List<EmployeeModel>();

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT * FROM (
	                         SELECT *, [my_dense] = DENSE_RANK() OVER (ORDER BY [Salary] DESC)  FROM [dbo].Employee) t WHERE T.[my_dense] = 1;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeModel result = new EmployeeModel()
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["First Name"],
                        MiddleName = string.IsNullOrEmpty(reader["Middle Name"]?.ToString()) ? "NA" : reader["Middle Name"]?.ToString(),
                        LastName = (string)reader["Last Name"],
                        DOB = (DateTime)reader["DOB"],
                        MobileNumber = (string)reader["Mobile Number"],
                        Address = string.IsNullOrEmpty(reader["Address"]?.ToString()) ? "NA" : reader["Address"].ToString(),
                        Salary = (decimal)reader["Salary"]
                    };

                    employeeHavingMaxSalary.Add(result);
                }
                return employeeHavingMaxSalary;
            }
        }

        public bool CreateStoreProcedureToAddEmployee()
        {
            bool isStoreProcedureAdded = false;

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"CREATE OR ALTER PROC uspInsertEmployee (@FName VARCHAR(50), @MName VARCHAR(50), @LName VARCHAR(50), @dob DATE, @MNumber VARCHAR(10), @Address VARCHAR(100), @Salary DECIMAL, @DesignationId INT) 
                             AS
                             BEGIN
	                             INSERT INTO [dbo].Employee ([First Name], [Middle Name], [Last Name], [DOB], [Mobile Number], [Address], [Salary], [DesignationId]) 
	                             VALUES 
	                             (@FName, @MName, @LName, @dob, @MNumber, @Address, @Salary, @DesignationId)
                             END;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if(cmd.ExecuteNonQuery() != 0)
                {
                    isStoreProcedureAdded = true;
                }  
            }
            return isStoreProcedureAdded;
        }

        public bool CreateProcedureToGetEmployeesOrderByDOB()
        {
            bool isStoreProcedureAdded = false;

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"CREATE OR ALTER PROC uspGetEmployeeOrderByDOB 
                            AS
                            BEGIN
	                            SELECT e.[Id], e.[First Name], e.[Middle Name], e.[Last Name], d.[Designation], e.[DOB], e.[Mobile Number], e.[Address], e.[Salary] 
	                            FROM
	                            [dbo].Employee e
	                            INNER JOIN [dbo].Designation d ON e.DesignationId = d.Id
	                            ORDER BY e.[DOB]
                            END;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    isStoreProcedureAdded = true;
                }
            }
            return isStoreProcedureAdded;
        }

        public bool CreateProcedureToGetEmployeesOrderByFname()
        {
            bool isStoreProcedureAdded = false;

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"CREATE OR ALTER PROC uspGetEmployeeByDesignationId (@DesignationId INT) 
                            AS
                            BEGIN
	                            SELECT e.[Id], e.[First Name], e.[Middle Name], e.[Last Name], e.[DOB], e.[Mobile Number], e.[Address], e.[Salary] 
	                            FROM
	                            [dbo].Employee e
	                            INNER JOIN [dbo].Designation d ON e.DesignationId = d.Id
	                            WHERE d.[Id] = @DesignationId
	                            ORDER BY e.[First Name]
                            END;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    isStoreProcedureAdded = true;
                }
            }
            return isStoreProcedureAdded;
        }

        public bool CreateViewToGetEmployeeList()
        {
            bool isViewAdded = false;

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"CREATE OR ALTER VIEW employee_info
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
	                            INNER JOIN [dbo].Designation d ON e.DesignationId = d.Id;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    isViewAdded = true;
                }
            }
            return isViewAdded;
        }

        public bool CreateNonClusteredIndexOnDepartmentIdForEmployee()
        {
            bool isIndexAdded = false;

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"DROP INDEX IF EXISTS ix_employee_designationId ON [dbo].Employee; CREATE NONCLUSTERED INDEX ix_employee_designationId ON [dbo].Employee([DesignationId]);";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    isIndexAdded = true;
                }
            }
            return isIndexAdded;
        }
    }
}