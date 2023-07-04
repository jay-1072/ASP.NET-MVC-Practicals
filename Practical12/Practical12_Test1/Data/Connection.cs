using Practical12_Test1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Practical12_Test1.Data
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

                while(reader.Read())
                {
                    Employee employee = new Employee()
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["First Name"],
                        MiddleName =  String.IsNullOrEmpty(reader["Middle Name"]?.ToString()) ? "NA" : reader["Middle Name"].ToString(),
                        LastName = (string)reader["Last Name"],
                        DOB = (DateTime)reader["DOB"],
                        MobileNumber = (string)reader["Mobile Number"],
                        Address = String.IsNullOrEmpty(reader["Address"]?.ToString()) ? "NA" : reader["Address"].ToString()
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

                string sql = $"INSERT INTO [dbo].Employee ([First Name], [Middle Name], [Last Name], [DOB], [Mobile Number], [Address]) VALUES (@fName, @mName, @lName, @dob, @mNumber, @address);";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fName", employee.FirstName);
                cmd.Parameters.AddWithValue("@mName", employee.MiddleName??"NA");
                cmd.Parameters.AddWithValue("@lName", employee.LastName);
                cmd.Parameters.AddWithValue("@dob", employee.DOB);
                cmd.Parameters.AddWithValue("@mNumber", employee.MobileNumber);
                cmd.Parameters.AddWithValue("@address", employee.Address??"NA");
                
                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }

        public int AddDummyEmployees()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"INSERT INTO [dbo].Employee ([First Name], [Middle Name], [Last Name], [DOB], [Mobile Number], [Address]) VALUES 
                             ('Jay', 'aaa', 'Koshti', '2001-10-24', '123456789', 'address1'),
                             ('Ajay', 'bbb', 'Koshti', '2001-11-23', '123123123', 'address2'),
                             ('Sanjay', 'ccc', 'Koshti', '2001-9-1', '123412341', 'address3'),
                             ('Preety', 'ddd', 'Koshti', '2001-10-24', '121212121', 'address4');";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }

        public int ChangeFirstNameForFirstRecord()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "UPDATE [dbo].Employee SET [First Name]='SQLPerson' WHERE Id = 1;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }

        public int ChangeAllMiddleName()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "UPDATE [dbo].Employee SET [Middle Name]='I'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }

        public int DeleteRecordWithIdLessThan2()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM [dbo].Employee WHERE Id < 2;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }

        public int DeleteAllEmployee()
        {
            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = $"TRUNCATE TABLE [dbo].Employee;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected;
            }
        }
    }
}