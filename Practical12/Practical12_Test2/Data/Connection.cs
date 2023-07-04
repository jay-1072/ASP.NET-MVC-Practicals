using Practical12_Test2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Practical12_Test2.Data
{
    public class Connection
    {
        SqlConnection conn = null;
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private List<Employee> _employees = new List<Employee>();

        public List<Employee> FetchEmployees()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM [dbo].Employee;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee()
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["First Name"],
                        MiddleName = String.IsNullOrEmpty(reader["Middle Name"]?.ToString()) ? "NA" : reader["Middle Name"].ToString(),
                        LastName = (string)reader["Last Name"],
                        DOB = (DateTime)reader["DOB"],
                        MobileNumber = (string)reader["Mobile Number"],
                        Address = String.IsNullOrEmpty(reader["Address"]?.ToString()) ? "NA" : reader["Address"].ToString(),
                        Salary = (decimal)reader["Salary"]
                    };

                    _employees.Add(employee);
                }
                return _employees;
            }
        }

        public int AddEmployee(Employee employee)
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = $"INSERT INTO [dbo].Employee ([First Name], [Middle Name], [Last Name], [DOB], [Mobile Number], [Address], [Salary]) VALUES (@fName, @mName, @lName, @dob, @mNumber, @address, @salary);";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fName", employee.FirstName);
                cmd.Parameters.AddWithValue("@mName", employee.MiddleName??"NA");
                cmd.Parameters.AddWithValue("@lName", employee.LastName);
                cmd.Parameters.AddWithValue("@dob", employee.DOB);
                cmd.Parameters.AddWithValue("@mNumber", employee.MobileNumber);
                cmd.Parameters.AddWithValue("@address", employee.Address??"NA");
                cmd.Parameters.AddWithValue("@salary", employee.Salary);

                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }

        public int CountEmployeeHavingMiddleNameNull()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = $"SELECT COUNT([Id]) FROM [dbo].Employee WHERE [Middle Name]='NA' OR [Middle Name]='';";

                SqlCommand cmd = new SqlCommand(sql, conn);


                int rowAffected = (int)cmd.ExecuteScalar();

                return rowAffected;
            }
        }

        public decimal TotalSalaryOfEmployee()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT SUM([Salary]) FROM [dbo].Employee;";

                SqlCommand cmd = new SqlCommand(sql, conn);

                decimal TotalSalary = (decimal)cmd.ExecuteScalar();

                return TotalSalary;
            }
        }

        public List<Employee> EmployeeDateOfBirthLessThan1_1_2000()
        {
            List<Employee> temp = new List<Employee>();
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "select * from employee where DOB < '2000-01-01';";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee()
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["First Name"],
                        MiddleName = String.IsNullOrEmpty(reader["Middle Name"]?.ToString()) ? "NA" : reader["Middle Name"].ToString(),
                        LastName = (string)reader["Last Name"],
                        DOB = (DateTime)reader["DOB"],
                        MobileNumber = (string)reader["Mobile Number"],
                        Address = String.IsNullOrEmpty(reader["Address"]?.ToString()) ? "NA" : reader["Address"].ToString(),
                        Salary = (decimal)reader["Salary"]
                    };

                    temp.Add(employee);
                }
                return temp;
            }
        }

    }
}