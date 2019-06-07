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

                command.Parameters.Add("@PlaceAddress", SqlDbType.NVarChar);
                command.Parameters["@PlaceAddres"].Value = delivery.PlaceAddress;

                command.Parameters.Add("@DeliveredByEmployeeID", SqlDbType.Int);
                command.Parameters["@DeliveredByEmployeeID"].Value = delivery.DeliveredByEmployeeID;

                command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime);
                command.Parameters["@DeliveryDate"].Value = delivery.DeliveryDate;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
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

                command.Parameters.Add("@PlaceAddress", SqlDbType.NVarChar);
                command.Parameters["@PlaceAddress"].Value = delivery.PlaceAddress;

                command.Parameters.Add("@DeliveredByEmployeeID", SqlDbType.Int);
                command.Parameters["@DeliveredByEmployeeID"].Value = delivery.DeliveredByEmployeeID;

                command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime);
                command.Parameters["@DeliveryDate"].Value = delivery.DeliveryDate;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
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
                connection.Close();
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

                Delivery Delivery = null;
                while (reader.Read())
                {
                    Delivery = new Delivery();

                    Delivery.DeliveryID = Convert.ToInt32(reader.GetValue(1));

                    Delivery.DetaineeID = Convert.ToInt32(reader.GetValue(2));

                    Delivery.DetentionID = Convert.ToInt32(reader.GetValue(3));

                    Delivery.PlaceAddress = reader.GetValue(4).ToString();

                    Delivery.DeliveredByEmployeeID = Convert.ToInt32(reader.GetValue(5));

                    Delivery.DeliveryDate = Convert.ToDateTime(reader.GetValue(6));

                }
                connection.Close();
                return Delivery;
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
                Delivery Delivery = null;

                List<Delivery> Deliverys_list = new List<Delivery>();
                while (reader.Read())
                {
                    Delivery = new Delivery
                    {
                        DeliveryID = Convert.ToInt32(reader.GetValue(0)),

                        DetaineeID = Convert.ToInt32(reader.GetValue(1)),

                        DetentionID = Convert.ToInt32(reader.GetValue(2)),

                        PlaceAddress = reader.GetValue(3).ToString(),

                        DeliveredByEmployeeID = Convert.ToInt32(reader.GetValue(4)),

                        DeliveryDate = Convert.ToDateTime(reader.GetValue(5))
                    };

                    Deliverys_list.Add(Delivery);
                }
                connection.Close();
                return Deliverys_list;
            }
        }
    }
}