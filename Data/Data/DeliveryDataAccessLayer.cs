using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Detention_facility.Data
{
    public class DeliveryDataAccessLayer : IDeliveryDataAccess
    {
        public void InsertDelivery(Delivery delivery)
        {
            const string storedProcedureName = Constants.InsertDelivery;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = delivery.DetaineeID;

                command.Parameters.Add(Constants.DetentionID, SqlDbType.Int);
                command.Parameters[Constants.DetentionID].Value = delivery.DetentionID;

                command.Parameters.Add(Constants.PlaceAddress, SqlDbType.VarChar);
                command.Parameters[Constants.PlaceAddress].Value = delivery.PlaceAddress;

                command.Parameters.Add(Constants.@DeliveredByEmployeeID, SqlDbType.Int);
                command.Parameters[Constants.DeliveredByEmployeeID].Value = delivery.DeliveredByEmployeeID;

                command.Parameters.Add(Constants.DeliveryDate, SqlDbType.DateTime);
                command.Parameters[Constants.DeliveryDate].Value = delivery.DeliveryDate;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateDelivery(int id, Delivery delivery)
        {
            const string storedProcedureName = Constants.UpdateDelivery;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DeliveryID, SqlDbType.Int);
                command.Parameters[Constants.DeliveryID].Value = id;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = delivery.DetaineeID;

                command.Parameters.Add(Constants.DetentionID, SqlDbType.Int);
                command.Parameters[Constants.DetentionID].Value = delivery.DetentionID;

                command.Parameters.Add(Constants.PlaceAddress, SqlDbType.VarChar);
                command.Parameters[Constants.PlaceAddress].Value = delivery.PlaceAddress;

                command.Parameters.Add(Constants.DeliveredByEmployeeID, SqlDbType.Int);
                command.Parameters[Constants.DeliveredByEmployeeID].Value = delivery.DeliveredByEmployeeID;

                command.Parameters.Add(Constants.DeliveryDate, SqlDbType.DateTime);
                command.Parameters[Constants.DeliveryDate].Value = delivery.DeliveryDate;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteDelivery(int id)
        {
            const string storedProcedureName = Constants.DeleteDelivery;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DeliveryID, SqlDbType.Int);
                command.Parameters[Constants.DeliveryID].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Delivery GetDeliveryByID(int id)
        {
            const string storedProcedureName = Constants.GetDeliveryByID;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DeliveryID, SqlDbType.Int);
                command.Parameters[Constants.DeliveryID].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Delivery delivery = null;
                while (reader.Read())
                {
                    delivery = new Delivery
                    {
                        DeliveryID = Convert.ToInt32(reader.GetValue(0)),

                        DetaineeID = Convert.ToInt32(reader.GetValue(1)),

                        DetentionID = Convert.ToInt32(reader.GetValue(2)),

                        PlaceAddress = reader.GetValue(3).ToString(),

                        DeliveredByEmployeeID = Convert.ToInt32(reader.GetValue(4)),

                        DeliveryDate = Convert.ToDateTime(reader.GetValue(5))
                    };
                }
                connection.Close();
                return delivery;
            }
        }


        public Delivery GetDeliveryByIDs(int detaineeID, int detentionID)
        {
            const string storedProcedureName = Constants.GetDeliveriesByIDs;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = detaineeID;

                command.Parameters.Add(Constants.DetentionID, SqlDbType.Int);
                command.Parameters[Constants.DetentionID].Value = detentionID;


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Delivery delivery = null;
                while (reader.Read())
                {
                    delivery = new Delivery
                    {
                        DeliveryID = Convert.ToInt32(reader.GetValue(0)),

                        DeliveryDate = Convert.ToDateTime(reader.GetValue(1)),

                        PlaceAddress = reader.GetValue(2).ToString(),
                    
                        DeliveredByEmployeeID = Convert.ToInt32(reader.GetValue(3))                 
                    };
                }
                connection.Close();
                return delivery;
            }
        }

        public SmartDelivery GetSmartDeliveryByIDs(int detaineeID, int detentionID)
        {
            const string storedProcedureName = Constants.GetSmartDeliveriesByIDs;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Constants.DetaineeID, SqlDbType.Int);
                command.Parameters[Constants.DetaineeID].Value = detaineeID;

                command.Parameters.Add(Constants.DetentionID, SqlDbType.Int);
                command.Parameters[Constants.DetentionID].Value = detentionID;


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                SmartDelivery delivery = null;
                while (reader.Read())
                {
                    delivery = new SmartDelivery
                    {
                        DeliveryID = Convert.ToInt32(reader.GetValue(0)),

                        DeliveryDate = Convert.ToDateTime(reader.GetValue(1)),

                        PlaceAddress = reader.GetValue(2).ToString(),

                        EmployeeFullName = reader.GetValue(3).ToString()
                    };
                }
                connection.Close();
                return delivery;
            }
        }


        public List<Delivery> GetDeliveries()
        {
            const string storedProcedureName = Constants.GetDeliveriesOfDetainees;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Delivery delivery = null;

                List<Delivery> deliveriesList = new List<Delivery>();
                while (reader.Read())
                {
                    delivery = new Delivery
                    {
                        DeliveryID = Convert.ToInt32(reader.GetValue(0)),

                        DetaineeID = Convert.ToInt32(reader.GetValue(1)),

                        DetentionID = Convert.ToInt32(reader.GetValue(2)),

                        PlaceAddress = reader.GetValue(3).ToString(),

                        DeliveredByEmployeeID = Convert.ToInt32(reader.GetValue(4)),

                        DeliveryDate = Convert.ToDateTime(reader.GetValue(5))
                    };

                    deliveriesList.Add(delivery);
                }
                connection.Close();
                return deliveriesList;
            }
        }
    }
}