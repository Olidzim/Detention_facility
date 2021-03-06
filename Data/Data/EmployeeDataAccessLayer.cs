﻿using Detention_facility.Models;
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
            const string storedProcedureName = Constants.InsertEmployee;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.FirstName, SqlDbType.NVarChar);
                command.Parameters["@FirstName"].Value = Employee.FirstName;

                command.Parameters.Add(Constants.LastName, SqlDbType.NVarChar);
                command.Parameters["@LastName"].Value = Employee.LastName;

                command.Parameters.Add(Constants.Patronymic, SqlDbType.NVarChar);
                command.Parameters["@Patronymic"].Value = Employee.Patronymic;
                
                command.Parameters.Add(Constants.Position, SqlDbType.NVarChar);
                command.Parameters["@Position"].Value = Employee.Position;

                command.Parameters.Add(Constants.EmployeeRank, SqlDbType.NVarChar);
                command.Parameters["@EmployeeRank"].Value = Employee.EmployeeRank;
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateEmployee(int id, Employee Employee)
        {
            const string storedProcedureName = Constants.UpdateEmployee;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.EmployeeID, SqlDbType.Int);
                command.Parameters["@EmployeeID"].Value = id;

                command.Parameters.Add(Constants.FirstName, SqlDbType.NVarChar);
                command.Parameters["@FirstName"].Value = Employee.FirstName;

                command.Parameters.Add(Constants.LastName, SqlDbType.NVarChar);
                command.Parameters["@LastName"].Value = Employee.LastName;

                command.Parameters.Add(Constants.Patronymic, SqlDbType.NVarChar);
                command.Parameters["@Patronymic"].Value = Employee.Patronymic;

                command.Parameters.Add(Constants.Position, SqlDbType.NVarChar);
                command.Parameters["@Position"].Value = Employee.Position;

                command.Parameters.Add(Constants.EmployeeRank, SqlDbType.NVarChar);
                command.Parameters["@EmployeeRank"].Value = Employee.EmployeeRank;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteEmployee(int id)
        {
            const string storedProcedureName = Constants.DeleteEmployee;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.EmployeeID, SqlDbType.Int);
                command.Parameters["@EmployeeID"].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Employee GetEmployeeByID(int id)
        {
            const string storedProcedureName = Constants.GetEmployeeByID;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.EmployeeID, SqlDbType.Int);
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
            const string storedProcedureName = Constants.GetEmployees;
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

        public List<SmartEmployee> Employees(string term)
        {
            const string storedProcedureName = Constants.EmployeeSearch;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add(Constants.term, SqlDbType.VarChar);
                command.Parameters["@term"].Value = term;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                SmartEmployee Employee = null;

                List<SmartEmployee> Employees_list = new List<SmartEmployee>();
                while (reader.Read())
                {
                    Employee = new SmartEmployee
                    {
                        EmployeeID = Convert.ToInt32(reader.GetValue(0)),

                        FullName = reader.GetValue(1).ToString(),

                        EmployeeRank = reader.GetValue(2).ToString(),

                        Position = reader.GetValue(3).ToString()
                    };
                    Employees_list.Add(Employee);
                }
                connection.Close();
                return Employees_list;
            }
        }
    }
}

