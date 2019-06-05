﻿using Detention_facility.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Detention_facility.Data;

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

                command.Parameters.Add("@PlaceID", SqlDbType.Int);
                command.Parameters["@PlaceID"].Value = release.PlaceID;

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

                command.Parameters.Add("@PlaceID", SqlDbType.Int);
                command.Parameters["@PlaceID"].Value = release.PlaceID;

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
                    Release = new Release();

                    Release.ReleaseID = Convert.ToInt32(reader.GetValue(1));

                    Release.DetaineeID = Convert.ToInt32(reader.GetValue(2));

                    Release.DetentionID = Convert.ToInt32(reader.GetValue(3));

                    Release.PlaceID = Convert.ToInt32(reader.GetValue(4));

                    Release.ReleasedByEmployeeID = Convert.ToInt32(reader.GetValue(5));

                    Release.ReleaseDate = Convert.ToDateTime(reader.GetValue(6));

                }
                connection.Close();
                return Release;
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
                        ReleaseID = Convert.ToInt32(reader.GetValue(1)),

                        DetaineeID = Convert.ToInt32(reader.GetValue(2)),

                        DetentionID = Convert.ToInt32(reader.GetValue(3)),

                        PlaceID = Convert.ToInt32(reader.GetValue(4)),

                        ReleasedByEmployeeID = Convert.ToInt32(reader.GetValue(5)),

                        ReleaseDate = Convert.ToDateTime(reader.GetValue(6))
                    };

                    Releases_list.Add(Release);
                }
                connection.Close();
                return Releases_list;
            }
        }
    }
}