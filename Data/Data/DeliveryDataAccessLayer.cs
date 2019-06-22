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
            const string storedProcedureName = "InsertDelivery";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = delivery.DetaineeID;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = delivery.DetentionID;

                command.Parameters.Add("@PlaceAddress", SqlDbType.VarChar);
                command.Parameters["@PlaceAddress"].Value = delivery.PlaceAddress;

                command.Parameters.Add("@DeliveredByEmployeeID", SqlDbType.Int);
                command.Parameters["@DeliveredByEmployeeID"].Value = delivery.DeliveredByEmployeeID;

                command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime);
                command.Parameters["@DeliveryDate"].Value = delivery.DeliveryDate;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateDelivery(int id, Delivery delivery)
        {
            const string storedProcedureName = "UpdateDelivery";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DeliveryID", SqlDbType.Int);
                command.Parameters["@DeliveryID"].Value = id;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = delivery.DetaineeID;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = delivery.DetentionID;

                command.Parameters.Add("@PlaceAddress", SqlDbType.VarChar);
                command.Parameters["@PlaceAddress"].Value = delivery.PlaceAddress;

                command.Parameters.Add("@DeliveredByEmployeeID", SqlDbType.Int);
                command.Parameters["@DeliveredByEmployeeID"].Value = delivery.DeliveredByEmployeeID;

                command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime);
                command.Parameters["@DeliveryDate"].Value = delivery.DeliveryDate;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteDelivery(int id)
        {
            const string storedProcedureName = "DeleteDelivery";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DeliveryID", SqlDbType.Int);
                command.Parameters["@DeliveryID"].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Delivery GetDeliveryByID(int id)
        {
            const string storedProcedureName = "GetDeliveryByID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DeliveryID", SqlDbType.Int);
                command.Parameters["@DeliveryID"].Value = id;

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
        public List<Delivery> GetDeliveries()
        {
            const string storedProcedureName = "GetDeliveriesOfDetainees";
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