using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Detention_facility.Data
{
    public class DetaineeDataAccessLayer : IDetaineeDataAccess
    {
        public void InsertDetainee(Detainee detainee)
        {
            const string storedProcedureName = "InsertDetainee";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
                command.Parameters["@FirstName"].Value = detainee.FirstName;

                command.Parameters.Add("@LastName", SqlDbType.NVarChar);
                command.Parameters["@LastName"].Value = detainee.LastName;

                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar);
                command.Parameters["@Patronymic"].Value = detainee.Patronymic;

                command.Parameters.Add("@Birthdate", SqlDbType.DateTime);
                command.Parameters["@Birthdate"].Value = detainee.BirthDate;

                command.Parameters.Add("@MaritalStatus", SqlDbType.NVarChar);
                command.Parameters["@MaritalStatus"].Value = detainee.MaritalStatus;

                command.Parameters.Add("@Job", SqlDbType.NVarChar);
                command.Parameters["@Job"].Value = detainee.Job;

                command.Parameters.Add("@MobilePhoneNumber", SqlDbType.NVarChar);
                command.Parameters["@MobilePhoneNumber"].Value = detainee.MobilePhoneNumber;

                command.Parameters.Add("@HomePhoneNumber", SqlDbType.NVarChar);
                command.Parameters["@HomePhoneNumber"].Value = detainee.HomePhoneNumber;

                command.Parameters.Add("@Photo", SqlDbType.Image);
                command.Parameters["@Photo"].Value = System.Convert.FromBase64String(detainee.Photo);

                command.Parameters.Add("@ExtraInfo", SqlDbType.NVarChar);
                command.Parameters["@ExtraInfo"].Value = detainee.ExtraInfo;

                command.Parameters.Add("@ResidencePlace", SqlDbType.NVarChar);
                command.Parameters["@ResidencePlace"].Value = detainee.ResidencePlace;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddDetaineeToDetention(int detaineeID, int detentionID)
        {
            const string storedProcedureName = "AddDetaineeToDetention";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = detaineeID;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = detentionID;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool CheckDetaineeInDetention(int detaineeID, int detentionID)
        {
            const string storedProcedureName = "CheckDetaineeInDetention";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = detaineeID;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = detentionID;

                connection.Open();
                if (command.ExecuteScalar() == null)
                {
                    connection.Close();
                    return false;
                }
                else
                {
                    connection.Close();
                    return true;
                }                
            }        
        }

        public void UpdateDetainee(int id, Detainee detainee)
        {

            const string storedProcedureName = "UpdateDetainee";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = id;

                command.Parameters.Add("@FirstName", SqlDbType.NVarChar);
                command.Parameters["@FirstName"].Value = detainee.FirstName;

                command.Parameters.Add("@LastName", SqlDbType.NVarChar);
                command.Parameters["@LastName"].Value = detainee.LastName;

                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar);
                command.Parameters["@Patronymic"].Value = detainee.Patronymic;

                command.Parameters.Add("@Birthdate", SqlDbType.DateTime);
                command.Parameters["@Birthdate"].Value = detainee.BirthDate;

                command.Parameters.Add("@MaritalStatus", SqlDbType.NVarChar);
                command.Parameters["@MaritalStatus"].Value = detainee.MaritalStatus;

                command.Parameters.Add("@Job", SqlDbType.NVarChar);
                command.Parameters["@Job"].Value = detainee.Job;

                command.Parameters.Add("@MobilePhoneNumber", SqlDbType.NVarChar);
                command.Parameters["@MobilePhoneNumber"].Value = detainee.MobilePhoneNumber;

                command.Parameters.Add("@HomePhoneNumber", SqlDbType.NVarChar);
                command.Parameters["@HomePhoneNumber"].Value = detainee.HomePhoneNumber;

                command.Parameters.Add("@Photo", SqlDbType.Image);
                command.Parameters["@Photo"].Value = detainee.Photo;

                command.Parameters.Add("@ExtraInfo", SqlDbType.NVarChar);
                command.Parameters["@ExtraInfo"].Value = detainee.ExtraInfo;

                command.Parameters.Add("@ResidencePlace", SqlDbType.NVarChar);
                command.Parameters["@ResidencePlace"].Value = detainee.ResidencePlace;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public void DeleteDetainee(int id)
        {

            const string storedProcedureName = "DeleteDetainee";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Detainee GetDetaineeByID(int id)
        {
            const string storedProcedureName = "GetDetaineeByID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Detainee Detainee = null;
                while (reader.Read())
                {
                    Detainee = new Detainee();
                    Detainee.DetaineeID = Convert.ToInt32(reader.GetValue(0));
                    Detainee.FirstName = reader.GetValue(1).ToString();
                    Detainee.LastName = reader.GetValue(2).ToString();
                    Detainee.Patronymic = reader.GetValue(3).ToString();
                    Detainee.BirthDate = Convert.ToDateTime(reader.GetValue(4));
                    Detainee.MaritalStatus = reader.GetValue(5).ToString();
                    Detainee.Job = reader.GetValue(6).ToString();
                    Detainee.MobilePhoneNumber = reader.GetValue(7).ToString();
                    Detainee.HomePhoneNumber = reader.GetValue(8).ToString();
                    if (reader.GetValue(9) == DBNull.Value)
                    {
                        Detainee.Photo = null;
                    }
                    else
                    {
                        Detainee.Photo = Convert.ToBase64String((byte[])reader.GetValue(9));
                    }
                    Detainee.ExtraInfo = reader.GetValue(10).ToString();
                    Detainee.ResidencePlace = reader.GetValue(11).ToString();
                }
                connection.Close();
                return Detainee;
            }
        }
        public List<Detainee> GetDetaineesByDetentionID(int id)
        {
            const string storedProcedureName = "GetDetaineesByDetentionID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Detainee Detainee = null;

                List<Detainee> Detainees_list = new List<Detainee>();
                while (reader.Read())
                {
                    Detainee = new Detainee();
                    Detainee.DetaineeID = Convert.ToInt32(reader.GetValue(0));
                    Detainee.FirstName = reader.GetValue(1).ToString();
                    Detainee.LastName = reader.GetValue(2).ToString();
                    Detainee.Patronymic = reader.GetValue(3).ToString();
                    Detainee.BirthDate = Convert.ToDateTime(reader.GetValue(4));
                    Detainee.MaritalStatus = reader.GetValue(5).ToString();
                    Detainee.Job = reader.GetValue(6).ToString();
                    Detainee.MobilePhoneNumber = reader.GetValue(7).ToString();
                    Detainee.HomePhoneNumber = reader.GetValue(8).ToString();
                    if (reader.GetValue(9) == DBNull.Value)
                    {
                        Detainee.Photo = null;
                    }
                    else
                    {
                        Detainee.Photo = Convert.ToBase64String((byte[])reader.GetValue(9));
                    }
                    Detainee.ExtraInfo = reader.GetValue(10).ToString();
                    Detainee.ResidencePlace = reader.GetValue(11).ToString();
                    Detainees_list.Add(Detainee);
                }
                connection.Close();
                return Detainees_list;
            }
        }
        public List<Detainee> GetDetainees()
        {
            const string storedProcedureName = "GetDetainees";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Detainee Detainee = null;

                List<Detainee> Detainees_list = new List<Detainee>();
                while (reader.Read())
                {
                    Detainee = new Detainee();

                    Detainee.DetaineeID = Convert.ToInt32(reader.GetValue(0));

                    Detainee.FirstName = reader.GetValue(1).ToString();

                    Detainee.LastName = reader.GetValue(2).ToString();

                    Detainee.Patronymic = reader.GetValue(3).ToString();

                    Detainee.BirthDate = Convert.ToDateTime(reader.GetValue(4));

                    Detainee.MaritalStatus = reader.GetValue(5).ToString();

                    Detainee.Job = reader.GetValue(6).ToString();

                    Detainee.MobilePhoneNumber = reader.GetValue(7).ToString();

                    Detainee.HomePhoneNumber = reader.GetValue(8).ToString();

                    if (reader.GetValue(9) == DBNull.Value)
                    {
                        Detainee.Photo = null;
                    }
                    else
                    {
                        Detainee.Photo = Convert.ToBase64String((byte[])reader.GetValue(9));
                    }

                    Detainee.ExtraInfo = reader.GetValue(10).ToString();

                    Detainee.ResidencePlace = reader.GetValue(11).ToString();

                    Detainees_list.Add(Detainee);
                }
                connection.Close();
                return Detainees_list;
            }
        }

    }
}
