using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Detention_facility.Data
{
    public class DetaineeDataAccessLayer : IDetaineeDataAccess
    {
        public int InsertDetainee(Detainee detainee)
        {
            const string storedProcedureName = Constants.InsertDetainee;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.FirstName, SqlDbType.NVarChar);
                command.Parameters[Constants.FirstName].Value = detainee.FirstName;

                command.Parameters.Add(Constants.LastName, SqlDbType.NVarChar);
                command.Parameters[Constants.LastName].Value = detainee.LastName;

                command.Parameters.Add(Constants.Patronymic, SqlDbType.NVarChar);
                command.Parameters[Constants.Patronymic].Value = detainee.Patronymic;

                command.Parameters.Add(Constants.Birthdate, SqlDbType.DateTime);
                command.Parameters[Constants.Birthdate].Value = detainee.BirthDate;

                command.Parameters.Add(Constants.MaritalStatus, SqlDbType.NVarChar);
                command.Parameters[Constants.MaritalStatus].Value = detainee.MaritalStatus;

                command.Parameters.Add(Constants.Job, SqlDbType.NVarChar);
                command.Parameters[Constants.Job].Value = detainee.Job;

                command.Parameters.Add(Constants.MobilePhoneNumber, SqlDbType.NVarChar);
                command.Parameters[Constants.MobilePhoneNumber].Value = detainee.MobilePhoneNumber;

                command.Parameters.Add(Constants.HomePhoneNumber, SqlDbType.NVarChar);
                command.Parameters[Constants.HomePhoneNumber].Value = detainee.HomePhoneNumber;

                command.Parameters.Add(Constants.Photo, SqlDbType.NVarChar);
                command.Parameters[Constants.Photo].Value = detainee.Photo;

                command.Parameters.Add(Constants.ExtraInfo, SqlDbType.NVarChar);
                command.Parameters[Constants.ExtraInfo].Value = detainee.ExtraInfo;

                command.Parameters.Add(Constants.ResidencePlace, SqlDbType.NVarChar);
                command.Parameters[Constants.ResidencePlace].Value = detainee.ResidencePlace;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                int id = 0;
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader.GetValue(0));
                }
                connection.Close();
                return id;
            }
        }

        public void AddDetaineeToDetention(int detaineeID, int detentionID)
        {
            const string storedProcedureName = Constants.AddDetaineeToDetention;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = detaineeID;

                command.Parameters.Add(Constants.DetentionID, SqlDbType.Int);
                command.Parameters[Constants.DetentionID].Value = detentionID;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<SmartDetainee> Detainees(string term)
        {
            const string storedProcedureName = Constants.DetaineeSearch;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add(Constants.term, SqlDbType.VarChar);
                command.Parameters[Constants.term].Value = term;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                SmartDetainee detainee = null;

                List<SmartDetainee> detainees_list = new List<SmartDetainee>();
                while (reader.Read())
                {
                    detainee = new SmartDetainee
                    {
                        DetaineeID = Convert.ToInt32(reader.GetValue(0)),

                        Fullname = reader.GetValue(1).ToString()

                    };
                    detainees_list.Add(detainee);
                }
                connection.Close();
                return detainees_list;
            }
        }


        public bool CheckDetaineeInDetention(int detaineeID, int detentionID)
        {
            const string storedProcedureName = Constants.CheckDetaineeInDetentionearch;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = detaineeID;

                command.Parameters.Add(Constants.DetentionID, SqlDbType.Int);
                command.Parameters[Constants.DetentionID].Value = detentionID;

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

            const string storedProcedureName = Constants.UpdateDetainee;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = id;

                command.Parameters.Add(Constants.FirstName, SqlDbType.NVarChar);
                command.Parameters[Constants.FirstName].Value = detainee.FirstName;

                command.Parameters.Add(Constants.LastName, SqlDbType.NVarChar);
                command.Parameters[Constants.LastName].Value = detainee.LastName;

                command.Parameters.Add(Constants.Patronymic, SqlDbType.NVarChar);
                command.Parameters[Constants.Patronymic].Value = detainee.Patronymic;

                command.Parameters.Add(Constants.Birthdate, SqlDbType.DateTime);
                command.Parameters[Constants.Birthdate].Value = detainee.BirthDate;

                command.Parameters.Add(Constants.MaritalStatus, SqlDbType.NVarChar);
                command.Parameters[Constants.MaritalStatus].Value = detainee.MaritalStatus;

                command.Parameters.Add(Constants.Job, SqlDbType.NVarChar);
                command.Parameters[Constants.Job].Value = detainee.Job;

                command.Parameters.Add(Constants.MobilePhoneNumber, SqlDbType.NVarChar);
                command.Parameters[Constants.MobilePhoneNumber].Value = detainee.MobilePhoneNumber;

                command.Parameters.Add(Constants.HomePhoneNumber, SqlDbType.NVarChar);
                command.Parameters[Constants.HomePhoneNumber].Value = detainee.HomePhoneNumber;

                command.Parameters.Add(Constants.Photo, SqlDbType.NVarChar);
                command.Parameters[Constants.Photo].Value = detainee.Photo;

                command.Parameters.Add(Constants.ExtraInfo, SqlDbType.NVarChar);
                command.Parameters[Constants.ExtraInfo].Value = detainee.ExtraInfo;

                command.Parameters.Add(Constants.ResidencePlace, SqlDbType.NVarChar);
                command.Parameters[Constants.ResidencePlace].Value = detainee.ResidencePlace;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        public int LastDetainee()
        {
            const string storedProcedureName = Constants.GetLastDetaineeID;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                int id = 0;
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader.GetValue(0));

                }
                connection.Close();
                return id;
            }
        }

        public void DeleteDetainee(int id)
        {

            const string storedProcedureName = Constants.DeleteDetainee;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Detainee GetDetaineeByID(int id)
        {
            const string storedProcedureName = Constants.GetDetaineeByID;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = id;

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
                    /*   if (reader.GetValue(9) == DBNull.Value)
                       {
                           Detainee.Photo = null;
                       }
                       else
                       {
                           Detainee.Photo = Convert.ToBase64String((byte[])reader.GetValue(9));
                       }*/

                    Detainee.Photo = reader.GetValue(9).ToString();
                    Detainee.ExtraInfo = reader.GetValue(10).ToString();
                    Detainee.ResidencePlace = reader.GetValue(11).ToString();
                }
                connection.Close();
                return Detainee;
            }
        }

        public List<SmartDetainee> GetDetaineesByDetentionID(int id)
        {
            const string storedProcedureName = Constants.GetDetaineesByDetentionID;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetentionID, SqlDbType.Int);
                command.Parameters[Constants.DetentionID].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                SmartDetainee detainee = null;

                List<SmartDetainee> detainees_list = new List<SmartDetainee>();
                while (reader.Read())
                {
                    detainee = new SmartDetainee();
                    detainee.DetaineeID = Convert.ToInt32(reader.GetValue(0));
                    detainee.Fullname = reader.GetValue(1).ToString();
                    detainee.BirthDate = Convert.ToDateTime(reader.GetValue(2));
                    detainee.MaritalStatus = reader.GetValue(3).ToString();
                    detainee.Job = reader.GetValue(4).ToString();
                    detainee.MobilePhoneNumber = reader.GetValue(5).ToString();
                    detainee.HomePhoneNumber = reader.GetValue(6).ToString();
                    detainee.ResidencePlace = reader.GetValue(7).ToString();
                    detainees_list.Add(detainee);
                }
                connection.Close();
                return detainees_list;
            }
        }

        public List<SmartDetainee> GetDetaineesByAddress(string term)
        {
            const string storedProcedureName = Constants.DetaineeSearchByAddres;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add(Constants.term, SqlDbType.VarChar);
                command.Parameters[Constants.term].Value = term;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                SmartDetainee detainee = null;

                List<SmartDetainee> detainees_list = new List<SmartDetainee>();
                while (reader.Read())
                {
                    detainee = new SmartDetainee
                    {
                        DetaineeID = Convert.ToInt32(reader.GetValue(0)),

                        Fullname = reader.GetValue(1).ToString()

                    };
                    detainees_list.Add(detainee);
                }
                connection.Close();
                return detainees_list;
            }
        }

        public List<Detainee> GetDetainees()
        {
            const string storedProcedureName = Constants.GetDetainees;
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
                        Detainee.Photo = reader.GetValue(9).ToString();
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
