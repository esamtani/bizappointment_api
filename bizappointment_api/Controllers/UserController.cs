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

namespace bizappointment_api.Controllers
{
    public class UserController : ApiController
    {

        public HttpResultViewModel Login([FromBody] LoginViewModel _model)
        {
            DataSet ds;
            PasswordHash _pass = new PasswordHash();
            _model.password = _pass.ComputeSha256Hash(_model.password);
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "Invalid Credentials. Please try again!! ";
            DatabaseModel _dbrequest = new DatabaseModel();

            UserImplementation _user = new UserImplementation();

            if (_user.CheckUserExistsForLogin(_model))
            {
                _dbrequest.Request = _request;
                _dbrequest.Type = "Login";
                DatabaseConnection _conn = new DatabaseConnection();
                ds = _conn.ExecuteDataSet("SP.LoginModule", _dbrequest);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        var password = dr["password"].Equals(DBNull.Value) ? "" : dr["password"].ToString();
                        _model.logininfoid = dr["logininfoid"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr["logininfoid"]);

                        if (_model.password == password)
                        {
                            result.status = true;

                            _model = _user.GetUserLoginDetailsById(_model);
                            if (_model != null)
                            {
                                JWTImplementation jwt = new JWTImplementation();
                                _model.token = "Bearer " + jwt.createToken(_model.username);
                                _model.password = "";
                                result.status = true;
                                result.data = _model;
                                result.message = "You are successfully Logged In";
                            }
                        }
                        else
                        {
                            _model.errorCode = 2;
                            result.errorCode = _model.errorCode;
                            result.message = "Invalid Credentials. Please try again!!";
                        }
                    }
                }
            }
            else
            {
                _model.errorCode = 1;
                result.errorCode = _model.errorCode;
                result.status = false;
                result.message = "Sorry!! You are not registered on Selfhatch. Please Sign Up for Selfhatch";
            }
            return result;
        }
        public HttpResultViewModel ChangeUserPassword([FromBody] PasswordChangeViewModel _model)
        {
            DataSet ds;
            PasswordHash _pass = new PasswordHash();
            _model.password = _pass.ComputeSha256Hash(_model.password);
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();

            UserImplementation _user = new UserImplementation();
            bool issuccess = false;
            var pass = _user.GetPasswordForUser(_model);

            if (_model.oldpassword == pass)
            {
                _dbrequest.Request = _request;
                _dbrequest.Type = "ChangeUserPassword";
                DatabaseConnection _conn = new DatabaseConnection();
                ds = _conn.ExecuteDataSet("SP.LoginModule", _dbrequest);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        issuccess = dr["issuccess"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["issuccess"]);
                        result.data = issuccess;
                        result.status = true;
                        result.message = "Your Password has been changed successfully!.";
                    }
                }
                else
                {
                    result.status = false;
                    result.message = "There was an error while saving the data! Please try again..";
                }
            }
            else
            {
                result.status = false;
                result.message = "Old password does not match. Please type the correct password.";
            }
            return result;
        }
    }
}
