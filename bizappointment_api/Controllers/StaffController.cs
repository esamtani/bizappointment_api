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
    public class StaffController : ApiController
    {
        //method Definition
        public HttpResultViewModel CreateStaffDetails(StaffViewModel _model)
        {
            DataSet ds;
            bool issucess = false;
            //Request Serialization
            string _request = JsonConvert.SerializeObject(_model);
            //object Instantation
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while adding the Staff Name! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            //database Request setup
            _dbrequest.Type = "CreateStaffDetails";
            _dbrequest.Request = _request;
            //Database Querry Execution
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.StaffModule", _dbrequest);
                //DataProcessing
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows !=null && dt.Rows.Count>0)
                    {
                        DataRow dr = dt.Rows[0];
                        _model.staffmasterid = dr["staffmasterid"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["sattmasterid"]);
                        result.data = _model;
                        result.status = true;
                        result.message = "Sucessfully Inserted New Staff";
                    }

                }

            }

            //Error Handling
            catch(Exception ex)
            {
                SystemUtilities sysutiliti = new SystemUtilities();
                sysutiliti.SaveError(ex);
            }
            
            //Return Response
            return result;
        }



        //Method Definition
        public HttpResultViewModel GetStaffDetailById(StaffViewModel _model)
        {
            DataSet ds;
            //Request Serialization
            string _request = JsonConvert.SerializeObject(_model);

            //Object Instantation
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();


            //Database Request Setup
            _dbrequest.Request = _request;
            _dbrequest.Type = "GetStaffDetailById";

            //Database Querry Execution
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.StaffModule", _dbrequest);
                //Data Processing
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    result.data = dt;
                    result.status = true;

                }
            }
            //Error Handling
            catch(Exception ex)
            {
                SystemUtilities sysutiliti = new SystemUtilities();
                sysutiliti.SaveError(ex);

            }
              //Return Request
            return result;
        }




        public HttpResultViewModel EditStaffDetailsById([FromBody] StaffViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while saving the data! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "EditStaffDetailsById";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.StaffModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    issuccess = dr["issuccess"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["issuccess"]);
                    result.data = issuccess;
                    result.status = true;
                    //result.message = _model.requesttypecode + " has been updated successfully!";
                }
            }
            return result;
        }
        public HttpResultViewModel RemoveDepartmentById([FromBody] StaffViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "Department name has been deleted sucessfully.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "RemoveStaffById";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.StaffModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    issuccess = dr["issuccess"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["issuccess"]);
                    result.data = issuccess;
                    result.status = true;
                    result.message = "Department name has been Deleted successfully!";
                }
            }
            return result;
        }
        public HttpResultViewModel LoadAllStaffList([FromBody] StaffViewModel _model)
        {
            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "LoadAllStaffList";
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.StaffModule", _dbrequest);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    result.data = dt;
                    result.status = true;
                }
            }
            catch (Exception ex)
            {
                SystemUtilities systemutil = new SystemUtilities();
                systemutil.SaveError(ex);
            }
            return result;
        }

    }
}
