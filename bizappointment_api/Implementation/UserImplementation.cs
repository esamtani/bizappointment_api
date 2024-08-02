using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using bizappointment_api.Implementation;
using bizappointment_api.Models;
using bizappointment_api.Utilities;

namespace bizappointment_api.Implementation
{
    public class UserImplementation
    {
        // check whether the user is already exist or not while registering
        public bool CheckUserExists(RegistrationViewModel _model)
        {
            DataSet ds;
            bool _exists = false;
            string _request = JsonConvert.SerializeObject(_model);
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "CheckUserExists";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.LoginModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    _exists = dr["isexist"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["isexist"]);
                }

            }
            return _exists;
        }


        // check whether the user is already exist or not while login
        public bool CheckUserExistsForLogin(LoginViewModel _model)
        {
            DataSet ds;
            bool _exists = false;
            string _request = JsonConvert.SerializeObject(_model);
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "CheckUserExistsForLogin";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.LoginModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    _exists = dr["isexist"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["isexist"]);
                }

            }
            return _exists;
        }
        public LoginViewModel GetUserLoginDetailsById(LoginViewModel _model)
        {
            DataSet ds;
            bool _exists = false;
            string _request = JsonConvert.SerializeObject(_model);
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "GetUserLoginDetailsById";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.LoginModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    _model.username = dr["username"].Equals(DBNull.Value) ? "" : dr["username"].ToString();
                    _model.password = dr["password"].Equals(DBNull.Value) ? "" : dr["password"].ToString();
                    _model.userrolename = dr["rolename"].Equals(DBNull.Value) ? "" : dr["rolename"].ToString();
                    _model.userrolecode = dr["rolecode"].Equals(DBNull.Value) ? "" : dr["rolecode"].ToString();
                    _model.userroleid = dr["roleid"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["roleid"]);
                    _model.istemppassword = dr["istemppassword"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["istemppassword"]);
                    _model.logininfoid = dr["logininfoid"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr["logininfoid"]);
                    _model.userinfoid = dr["userinfoid"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr["userinfoid"]);

                    return _model;

                }

            }
            return null;
        }
        // to get users old password for comparing old & new password
        public string GetPasswordForUser(PasswordChangeViewModel _model)
        {
            DataSet ds;
            PasswordHash _pass = new PasswordHash();
            _model.oldpassword = _pass.ComputeSha256Hash(_model.oldpassword);
            var pass = "";
            string _request = JsonConvert.SerializeObject(_model);
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "GetPasswordForUser";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.LoginModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    pass = dr["password"].Equals(DBNull.Value) ? "" : Convert.ToString(dr["password"]);
                }

            }
            return pass;
        }
    }
}