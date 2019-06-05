using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Detention_facility.Data
{
    public class EmployeeDataAccessLayer : IEmployeeDataAccess
    {
        public void InsertEmployee(Employee Employee)
        {
            const string storedProcedureName = "InsertEmployee";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
                command.Parameters["@FirstName"].Value = Employee.FirstName;

                command.Parameters.Add("@LastName", SqlDbType.NVarChar);
                command.Parameters["@LastName"].Value = Employee.LastName;

                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar);
                command.Parameters["@Patronymic"].Value = Employee.Patronymic;
                
                command.Parameters.Add("@Position", SqlDbType.NVarChar);
                command.Parameters["@Position"].Value = Employee.Position;

                command.Parameters.Add("@EmployeeRank", SqlDbType.NVarChar);
                command.Parameters["@EmployeeRank"].Value = Employee.EmployeeRank;
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateEmployee(int id, Employee Employee)
        {
            const string storedProcedureName = "UpdateEmployee";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                command.Parameters["@EmployeeID"].Value = id;

                command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
                command.Parameters["@FirstName"].Value = Employee.FirstName;

                command.Parameters.Add("@LastName", SqlDbType.NVarChar);
                command.Parameters["@LastName"].Value = Employee.LastName;

                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar);
                command.Parameters["@Patronymic"].Value = Employee.Patronymic;

                command.Parameters.Add("@Position", SqlDbType.NVarChar);
                command.Parameters["@Position"].Value = Employee.Position;

                command.Parameters.Add("@EmployeeRank", SqlDbType.NVarChar);
                command.Parameters["@EmployeeRank"].Value = Employee.EmployeeRank;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteEmployee(int id)
        {
            const string storedProcedureName = "DeleteEmployee";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                command.Parameters["@EmployeeID"].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Employee GetEmployeeByID(int id)
        {
            const string storedProcedureName = "GetEmployeeByID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                command.Parameters["@EmployeeID"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Employee Employee = null;
                while (reader.Read())
                {
                    Employee = new Employee();
                    Employee.EmployeeID = Convert.ToInt32(reader.GetValue(0));
                    Employee.FirstName = reader.GetValue(1).ToString();
                    Employee.LastName = reader.GetValue(2).ToString();
                    Employee.Patronymic = reader.GetValue(3).ToString();
                    Employee.Position = reader.GetValue(4).ToString();
                    Employee.EmployeeRank = reader.GetValue(5).ToString();
                }
                connection.Close();
                return Employee;
            }
        }
        public List<Employee> GetEmployees()
        {
            const string storedProcedureName = "GetEmployees";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Employee Employee = null;

                List<Employee> Employees_list = new List<Employee>();
                while (reader.Read())
                {
                    Employee = new Employee
                    {
                        EmployeeID = Convert.ToInt32(reader.GetValue(0)),

                        FirstName = reader.GetValue(1).ToString(),

                        LastName = reader.GetValue(2).ToString(),

                        Patronymic = reader.GetValue(3).ToString(),

                        Position = reader.GetValue(4).ToString(),

                        EmployeeRank = reader.GetValue(5).ToString()
                    };
                    Employees_list.Add(Employee);
                }
                connection.Close();
                return Employees_list;
            }
        }
    }
}
