using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using T2PlanningApi.Models;

namespace T2PlanningApi.Database
{
    public class Database
    {
        public static DataTable ReadTable(string StoredProcedureName, Dictionary<string, object> para = null)
        {
            try
            {
                DataTable resultTable = new DataTable();

                string SQLConnectionString = ConfigurationManager.ConnectionStrings["T2PLANNING"].ConnectionString;
                SqlConnection connection = new SqlConnection(SQLConnectionString);

                connection.Open();

                SqlCommand sqlCmd = connection.CreateCommand();
                sqlCmd.Connection = connection;
                sqlCmd.CommandText = StoredProcedureName;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                if (para != null)
                {
                    foreach (KeyValuePair<string, object> data in para)
                    {
                        if (data.Value == null)
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, DBNull.Value);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, data.Value);
                        }
                    }
                }

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = sqlCmd;
                sqlDA.Fill(resultTable);

                connection.Close();

                return resultTable;
            }
            catch
            {
                return null;
            }
        }


        public static Response WriteTable(string StoredProcedureName, Dictionary<string, object> para = null)
        {
            Response response = new Response();
            try
            {
                string SQLConnectionString = ConfigurationManager.ConnectionStrings["T2PLANNING"].ConnectionString;
                SqlConnection connection = new SqlConnection(SQLConnectionString);


                SqlCommand sqlCmd = connection.CreateCommand();
                sqlCmd.Connection = connection;
                sqlCmd.CommandText = StoredProcedureName;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                if (para != null)
                {
                    foreach (KeyValuePair<string, object> data in para)
                    {
                        if (data.Value == null)
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, DBNull.Value);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, data.Value);
                        }
                    }
                }

                connection.Open();
                int i = sqlCmd.ExecuteNonQuery();
                connection.Close();
                if (i >= 1)
                {
                    response.Message = "Saved Successfully";
                    response.Status = 1;
                }
                else
                {
                    response.Message = "Faild To Save";
                    response.Status = 0;
                }
            }
            catch
            {
                response.Message = "Faild To Save";
                response.Status = 0;
            }
            return response;
        }

        public static DataTable WriteTb(string StoredProcedureName, Dictionary<string, object> para = null)
        {
            Response response = new Response();
            DataTable tableId = new DataTable();
            try
            {
                string SQLConnectionString = ConfigurationManager.ConnectionStrings["T2PLANNING"].ConnectionString;
                SqlConnection connection = new SqlConnection(SQLConnectionString);


                SqlCommand sqlCmd = connection.CreateCommand();
                sqlCmd.Connection = connection;
                sqlCmd.CommandText = StoredProcedureName;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                if (para != null)
                {
                    foreach (KeyValuePair<string, object> data in para)
                    {
                        if (data.Value == null)
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, DBNull.Value);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, data.Value);
                        }
                    }
                }

                connection.Open();
                //int i = sqlCmd.ExecuteNonQuery();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = sqlCmd;
                sqlDA.Fill(tableId);
                connection.Close();
                return tableId;
            }
            catch
            {
                return null;
            }
        }

        public static Response UpdateTable(string StoredProcedureName, Dictionary<string, object> para = null)
        {
            Response response = new Response();
            try
            {
                string SQLConnectionString = ConfigurationManager.ConnectionStrings["T2PLANNING"].ConnectionString;
                SqlConnection connection = new SqlConnection(SQLConnectionString);


                SqlCommand sqlCmd = connection.CreateCommand();
                sqlCmd.Connection = connection;
                sqlCmd.CommandText = StoredProcedureName;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                if (para != null)
                {
                    foreach (KeyValuePair<string, object> data in para)
                    {
                        if (data.Value == null)
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, DBNull.Value);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, data.Value);
                        }
                    }
                }

                connection.Open();
                int i = sqlCmd.ExecuteNonQuery();
                connection.Close();
                if (i >= 1)
                {
                    response.Message = "Update Successfully";
                    response.Status = 1;
                }
                else
                {
                    response.Message = "Faild To Update";
                    response.Status = 0;
                }
            }
            catch
            {
                response.Message = "Faild To Update";
                response.Status = 0;
            }
            return response;
        }

        public static Response DeleteTable(string StoredProcedureName, Dictionary<string, object> para = null)
        {
            Response response = new Response();
            try
            {
                string SQLConnectionString = ConfigurationManager.ConnectionStrings["T2PLANNING"].ConnectionString;
                SqlConnection connection = new SqlConnection(SQLConnectionString);


                SqlCommand sqlCmd = connection.CreateCommand();
                sqlCmd.Connection = connection;
                sqlCmd.CommandText = StoredProcedureName;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                if (para != null)
                {
                    foreach (KeyValuePair<string, object> data in para)
                    {
                        if (data.Value == null)
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, DBNull.Value);
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue('@' + data.Key, data.Value);
                        }
                    }
                }

                connection.Open();
                int i = sqlCmd.ExecuteNonQuery();
                connection.Close();
                if (i >= 1)
                {
                    response.Message = "Delete Successfully";
                    response.Status = 1;
                }
                else
                {
                    response.Message = "Faild To Delete";
                    response.Status = 0;
                }
            }
            catch
            {
                response.Message = "Faild To Delete";
                response.Status = 0;
            }
            return response;
        }
    }
}