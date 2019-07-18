using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Detention_facility.Data
{
    public class DetentionDataAccessLayer : IDetentionDataAccess
    {
        public void InsertDetention(Detention detention)
        {
            const string storedProcedureName = "InsertDetention";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetentionDate", SqlDbType.DateTime);
                command.Parameters["@DetentionDate"].Value = detention.DetentionDate;

                command.Parameters.Add("@DetainedByEmployeeID", SqlDbType.Int);
                command.Parameters["@DetainedByEmployeeID"].Value = detention.DetainedByEmployeeID;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateDetention(int id, Detention detention)
        {
            const string storedProcedureName = "UpdateDetention";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = id;

                command.Parameters.Add("@detentionDate", SqlDbType.DateTime);
                command.Parameters["@detentionDate"].Value = detention.DetentionDate;

                command.Parameters.Add("@DetainedByEmployeeID", SqlDbType.Int);
                command.Parameters["@DetainedByEmployeeID"].Value = detention.DetainedByEmployeeID;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteDetention(int id)
        {
            const string storedProcedureName = "DeleteDetention";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Detention GetDetentionByID(int id)
        {
            const string storedProcedureName = "GetDetentionByID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Detention detention = null;
                while (reader.Read())
                {
                    detention = new Detention();

                    detention.DetentionID = Convert.ToInt32(reader.GetValue(0));

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }

                    detention.DetainedByEmployeeID = Convert.ToInt32(reader.GetValue(2));
                }
                connection.Close();
                return detention;
            }
        }

        public List<Detention> GetDetentions()
        {
            const string storedProcedureName = "GetDetentions";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Detention detention = null;

                List<Detention> detentions_list = new List<Detention>();
                while (reader.Read())
                {
                    detention = new Detention
                    {
                        DetentionID = Convert.ToInt32(reader.GetValue(0))
                    };

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }

                    detention.DetainedByEmployeeID = Convert.ToInt32(reader.GetValue(2));

                    detentions_list.Add(detention);
                }
                connection.Close();
                return detentions_list;
            }
        }

        public List<SmartDetention> GetSmartDetentions()
        {
            const string storedProcedureName = "GetSmartDetentions";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                SmartDetention detention = null;

                List<SmartDetention> detentions_list = new List<SmartDetention>();
                while (reader.Read())
                {
                    detention = new SmartDetention
                    {
                        DetentionID = Convert.ToInt32(reader.GetValue(0))
                    };

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }

                    detention.EmployeeFullName = reader.GetValue(2).ToString();

                    detentions_list.Add(detention);
                }
                connection.Close();
                return detentions_list;
            }
        }


        public int LastDetention()
        {
            const string storedProcedureName = "LastDetentionID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                int id=0;
                while (reader.Read())
                {
                     id = Convert.ToInt32(reader.GetValue(0));

                }
                connection.Close();
                return id;
            }
        }

        public List<SmartDetention> GetSmartDetentionsByDetaineeID(int id)
        {
            const string storedProcedureName = "GetSmartDetentionsByDetaineeID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = id;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                SmartDetention detention = null;

                List<SmartDetention> detentions_list = new List<SmartDetention>();
                while (reader.Read())
                {
                    detention = new SmartDetention
                    {
                        DetentionID = Convert.ToInt32(reader.GetValue(0))
                    };

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }
                   
                    detention.EmployeeFullName = reader.GetValue(2).ToString();
                    detention.ReleaseStatus = reader.GetValue(3) == DBNull.Value ? "Не освобожден" : "Освобожден" ;
                    detention.DeliveryStatus = reader.GetValue(4) == DBNull.Value ? "Не доставлен" : "Доставлен";

                    detentions_list.Add(detention);
                }

                connection.Close();
                return detentions_list;
            }
        }

        public SmartDetention GetSmartDetentionsByDetentionID(int id)
        {
            const string storedProcedureName = "GetSmartDetentionsByDetentionID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Detention", SqlDbType.Int);
                command.Parameters["@Detention"].Value = id;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                SmartDetention detention = null;

                
                while (reader.Read())
                {
                    detention = new SmartDetention
                    {
                        DetentionID = Convert.ToInt32(reader.GetValue(0))
                    };

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }

                    detention.EmployeeFullName = reader.GetValue(2).ToString();    

                    
                }

                connection.Close();
                return detention;
            }
        }


        /*   public List<SmartDetention> GetSmartDetentions(int id)
           {
               const string storedProcedureName = "GetDetentionsByDetaineeID";
               using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
               {
                   SqlCommand command = new SqlCommand(storedProcedureName, connection);
                   command.CommandType = CommandType.StoredProcedure;

                   command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                   command.Parameters["@DetaineeID"].Value = id;

                   connection.Open();

                   SqlDataReader reader = command.ExecuteReader();
                   SmartDetention detention = null;

                   List<SmartDetention> detentions_list = new List<SmartDetention>();
                   while (reader.Read())
                   {
                       detention = new SmartDetention
                       {
                           DetentionID = Convert.ToInt32(reader.GetValue(0))
                       };

                       if (reader.GetValue(1) == DBNull.Value)
                       {
                           detention.DetentionDate = null;
                       }
                       else
                       {
                           detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                       }

                       detention.EmployeeFullName = reader.GetValue(2).ToString();

                       const string storedProcedureName2 = "GetDeliveriesByIDs";
                       SqlCommand command2 = new SqlCommand(storedProcedureName2, connection);
                       command2.CommandType = CommandType.StoredProcedure;
                       command2.Parameters.Add("@DetaineeID", SqlDbType.Int);
                       command2.Parameters["@DetaineeID"].Value = id;
                       command2.Parameters.Add("@DetentionID", SqlDbType.Int);
                       command2.Parameters["@DetentionID"].Value = detention.DetentionID;
                       SqlDataReader reader2 = command2.ExecuteReader();

                       if (reader2.HasRows)
                       {
                           detention.Delivery = "Доставлен";
                       }
                       else
                       {
                           detention.Delivery = "Не доставлен";
                       }
                       detentions_list.Add(detention);
                   }

                   connection.Close();
                   return detentions_list;
               }
           }*/


        public List<Detention> GetDetentionsByPlace(string place)
        {
            const string storedProcedureName = "GetDetentionsByResidencePlace";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                command.Parameters.Add("@ResidencePlace", SqlDbType.NVarChar);
                command.Parameters["@ResidencePlace"].Value = place;

                SqlDataReader reader = command.ExecuteReader();
                Detention detention = null;

                List<Detention> detentions_list = new List<Detention>();
                while (reader.Read())
                {
                    detention = new Detention
                    {
                        DetentionID = Convert.ToInt32(reader.GetValue(0))
                    };

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }

                    detention.DetainedByEmployeeID = Convert.ToInt32(reader.GetValue(2));

                    detentions_list.Add(detention);
                }
                connection.Close();
                return detentions_list;
            }
        }

        public List<Detention> GetDetentionsByLastName(string lastname)
        {
            const string storedProcedureName = "GetDetentionsByLastName";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                command.Parameters.Add("@LastName", SqlDbType.NVarChar);
                command.Parameters["@LastName"].Value = lastname;

                SqlDataReader reader = command.ExecuteReader();
                Detention detention = null;

                List<Detention> detentions_list = new List<Detention>();
                while (reader.Read())
                {
                    detention = new Detention
                    {
                        DetentionID = Convert.ToInt32(reader.GetValue(0))
                    };

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }

                    detention.DetainedByEmployeeID = Convert.ToInt32(reader.GetValue(2));

                    detentions_list.Add(detention);
                }
                connection.Close();
                return detentions_list;
            }
        }

        public List<SmartDetention> GetDetentionsByDate(DateTime date)
        {
            const string storedProcedureName = "GetSmartDetentionsByDate";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                command.Parameters.Add("@date", SqlDbType.DateTime);
                command.Parameters["@date"].Value = date;

                SqlDataReader reader = command.ExecuteReader();
                SmartDetention detention = null;

                List<SmartDetention> detentions_list = new List<SmartDetention>();
                while (reader.Read())
                {
                    detention = new SmartDetention
                    {
                        DetentionID = Convert.ToInt32(reader.GetValue(0))
                    };

                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        detention.DetentionDate = null;
                    }
                    else
                    {
                        detention.DetentionDate = Convert.ToDateTime(reader.GetValue(1));
                    }

                    detention.EmployeeFullName = reader.GetValue(2).ToString();

                    detentions_list.Add(detention);
                }
                connection.Close();
                return detentions_list;
            }
        }
    }
}
