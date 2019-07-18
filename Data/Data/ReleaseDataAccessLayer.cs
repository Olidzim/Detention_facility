﻿using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Detention_facility.Data
{
    public class ReleaseDataAccessLayer : IReleaseDataAccess
    {
        public void InsertRelease(Release release)
        {
            const string storedProcedureName = "InsertRelease";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = release.DetaineeID;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = release.DetentionID;

                command.Parameters.Add("@ReleasedByEmployeeID", SqlDbType.Int);
                command.Parameters["@ReleasedByEmployeeID"].Value = release.ReleasedByEmployeeID;

                command.Parameters.Add("@ReleaseDate", SqlDbType.DateTime);
                command.Parameters["@ReleaseDate"].Value = release.ReleaseDate;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateRelease(int id, Release release)
        {
            const string storedProcedureName = "UpdateRelease";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ReleaseID", SqlDbType.Int);
                command.Parameters["@ReleaseID"].Value = id;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = release.DetaineeID;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = release.DetentionID;

                command.Parameters.Add("@ReleasedByEmployeeID", SqlDbType.Int);
                command.Parameters["@ReleasedByEmployeeID"].Value = release.ReleasedByEmployeeID;

                command.Parameters.Add("@ReleaseDate", SqlDbType.DateTime);
                command.Parameters["@ReleaseDate"].Value = release.ReleaseDate;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteRelease(int id)
        {
            const string storedProcedureName = "DeleteRelease";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ReleaseID", SqlDbType.Int);
                command.Parameters["@ReleaseID"].Value = id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Release GetReleaseByID(int id)
        {
            const string storedProcedureName = "GetReleaseByID";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ReleaseID", SqlDbType.Int);
                command.Parameters["@ReleaseID"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Release Release = null;
                while (reader.Read())
                {
                    Release = new Release
                    {
                        ReleaseID = Convert.ToInt32(reader.GetValue(0)),

                        DetaineeID = Convert.ToInt32(reader.GetValue(1)),

                        ReleasedByEmployeeID = Convert.ToInt32(reader.GetValue(2)),

                        DetentionID = Convert.ToInt32(reader.GetValue(3)),

                        ReleaseDate = Convert.ToDateTime(reader.GetValue(4))
                    };

                }
                connection.Close();
                return Release;
            }
        }


        public SmartRelease GetReleaseByIDs(int detaineeID, int detentionID)
        {
            const string storedProcedureName = "GetReleasesByIDs";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetaineeID", SqlDbType.Int);
                command.Parameters["@DetaineeID"].Value = detaineeID;

                command.Parameters.Add("@DetentionID", SqlDbType.Int);
                command.Parameters["@DetentionID"].Value = detentionID;


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                SmartRelease release = null;
                while (reader.Read())
                {
                    release = new SmartRelease();


                    release.ReleaseID = Convert.ToInt32(reader.GetValue(0));

                    release.ReleaseDate = Convert.ToDateTime(reader.GetValue(1));

                    release.AmountAccrued = reader.GetValue(2) == DBNull.Value ? 0: Convert.ToInt32(reader.GetValue(2)) ;

                    release.AmountPaid = reader.GetValue(3) == DBNull.Value ? 0: Convert.ToInt32(reader.GetValue(3)) ;

                    release.EmployeeFullName = reader.GetValue(4).ToString();
                    
                }
                connection.Close();
                return release;
            }
        }

        public List<Release> GetReleases()
        {
            const string storedProcedureName = "GetReleasesOfDetainees";
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Release Release = null;

                List<Release> Releases_list = new List<Release>();
                while (reader.Read())
                {
                    Release = new Release
                    {
                        ReleaseID = Convert.ToInt32(reader.GetValue(0)),

                        DetaineeID = Convert.ToInt32(reader.GetValue(1)),

                        ReleasedByEmployeeID = Convert.ToInt32(reader.GetValue(2)),

                        DetentionID = Convert.ToInt32(reader.GetValue(3)),

                        ReleaseDate = Convert.ToDateTime(reader.GetValue(4))
                    };

                    Releases_list.Add(Release);
                }
                connection.Close();
                return Releases_list;
            }
        }
    }
}