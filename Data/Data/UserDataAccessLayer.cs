﻿using Detention_facility.Models;
using System.Data;
using System.Data.SqlClient;

namespace Detention_facility.Data
{
    public class UserDataAccessLayer : IUserDataAccessLayer
    {
        public void InsertUser(User user)
        {
            const string storedProcedureName = "InsertUser";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Login", SqlDbType.NVarChar);
                command.Parameters["@Login"].Value = user.Login;

                command.Parameters.Add("@Password", SqlDbType.NVarChar);
                command.Parameters["@Password"].Value = user.Password;

                command.Parameters.Add("@Role", SqlDbType.NVarChar);
                command.Parameters["@Role"].Value = user.Role;

                command.Parameters.Add("@Email", SqlDbType.NVarChar);
                command.Parameters["@Email"].Value = user.Email;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public User GetUserByID(int id)
        {
            const string storedProcedureName = "GetUserByID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                User user = null;
                while (reader.Read())
                {
                    user = new User();
                   
                    user.Login = reader.GetValue(1).ToString();                   

                    user.Password = reader.GetValue(2).ToString();

                    user.Role = reader.GetValue(3).ToString();

                    user.Email = reader.GetValue(4).ToString();
                }
                connection.Close();
                return user;
            }
        }

        public User CheckUser(string Login, string password)
        {
            const string storedProcedureName = "FindUser";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Login", SqlDbType.NVarChar);
                command.Parameters["@Login"].Value = Login;

                command.Parameters.Add("@Password", SqlDbType.NVarChar);
                command.Parameters["@Password"].Value = password;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                User checked_user = null;
                while (reader.Read())
                {
                    checked_user = new User();

                    checked_user.Login = reader.GetValue(1).ToString();

                    checked_user.Password = reader.GetValue(2).ToString();

                    checked_user.Role = reader.GetValue(3).ToString();

                    checked_user.Email = reader.GetValue(4).ToString();
                }
                connection.Close();
                return checked_user;
            }
        }

        public void UpdateUser(int id, User user)
        {
            const string storedProcedureName = "UpdateUser";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = id;

                command.Parameters.Add("@Login", SqlDbType.NVarChar);
                command.Parameters["@Login"].Value = user.Login;

                command.Parameters.Add("@Password", SqlDbType.NVarChar);
                command.Parameters["@Password"].Value = user.Password;

                command.Parameters.Add("@Role", SqlDbType.NVarChar);
                command.Parameters["@Role"].Value = user.Role;

                command.Parameters.Add("@Email", SqlDbType.NVarChar);
                command.Parameters["@Email"].Value = user.Email;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateUserPassword(int id, string password)
        {
            const string storedProcedureName = "UpdateUserPassword";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = id;

                command.Parameters.Add("@Password", SqlDbType.NVarChar);
                command.Parameters["@Password"].Value = password;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteUser(int id)
        {
            const string storedProcedureName = "DeleteUser";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
