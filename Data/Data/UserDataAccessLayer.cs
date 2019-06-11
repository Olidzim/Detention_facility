using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detention_facility.Models;

namespace Detention_facility.Data
{
    public class UserDataAccessLayer : IUserDataAccessLayer
    {
        public void InsertDetention(User user) 
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

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public User FindUser(string Login, string password)
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
                }
                connection.Close();
                return checked_user;
            }
        }
    }
}
