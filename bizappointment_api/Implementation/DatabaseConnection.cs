using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using bizappointment_api.Models;
using Newtonsoft.Json;

namespace bizappointment_api.Implementation
{
    public class DatabaseConnection
    {
        //public readonly string connetionString = @"Server=ME\SQLEXPRESS;Database= skoolbase; Integrated Security=True;";
        private int _userloginid = 0;
        private string connetionString = "";


        public DatabaseConnection()
        {
            string _server = WebConfigurationManager.AppSettings["DeploymentServer"];
            if (_server == "dev")
            {

                connetionString = @"Data Source=SQL6017.site4now.net;
                                                        Initial Catalog=db_a4e33b_stpeters;Integrated Security=False;
                                                        User Id=db_a4e33b_stpeters_admin;Password=Password#1;
                                                        MultipleActiveResultSets=True";
            }


            else if (_server == "qa")
            {
                connetionString = @"Data Source=SQL6017.site4now.net;
                                                        Initial Catalog=db_a4e33b_stpeters;Integrated Security=False;
                                                        User Id=db_a4e33b_stpeters_admin;Password=Password#1;
                                                        MultipleActiveResultSets=True";

            }

            else if (_server == "uat")
            {
                connetionString = @"Data Source=142.11.194.164\SQLEXPRESS,1433;
                                                Initial Catalog = selfhatch; Integrated Security = False;
                                                User Id = kantascrypt_admin; Password = K@ntascrypt#1;
                                                MultipleActiveResultSets = True";

            }

            else if (_server == "prod")
            {
                connetionString = @"Data Source=SQL6017.site4now.net;
                                                        Initial Catalog=db_a4e33b_stpeters;Integrated Security=False;
                                                        User Id=db_a4e33b_stpeters_admin;Password=Password#1;
                                                        MultipleActiveResultSets=True";

            }
        }


        public DataSet ExecuteDataSet(string _cmd, DatabaseModel _model)
        {
            string sql = _cmd;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command;
            DataSet ds = new DataSet();
            command = new SqlCommand(sql, cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Request", _model.Request);
            command.Parameters.AddWithValue("@Type", _model.Type);
            command.Parameters.AddWithValue("@LoggedinId", _userloginid);
            command.CommandTimeout = 1200;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();
            cnn.Close();
            return ds;
        }

        public void ExecuteNonQuery(string _cmd, DatabaseModel _model)
        {

            string sql = _cmd;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command;
            DataSet ds = new DataSet();
            command = new SqlCommand(sql, cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Request", _model.Request);
            command.Parameters.AddWithValue("@Type", _model.Type);
            command.Parameters.AddWithValue("@LoggedinId", _userloginid);
            command.ExecuteNonQuery();
            command.Dispose();
            cnn.Close();
        }

        public DataSet ExecuteDataSetWithKey(string _cmd, string type, int key)
        {

            dynamic requestobj = new ExpandoObject();
            requestobj.id = key;
            string _request = JsonConvert.SerializeObject(requestobj);
            string sql = _cmd;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command;
            DataSet ds = new DataSet();
            command = new SqlCommand(sql, cnn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Request", _request);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@LoggedinId", _userloginid);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            adapter.Dispose();
            command.Dispose();
            cnn.Close();
            return ds;
        }


    }
}