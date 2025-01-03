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
    public class AppointmentController : ApiController
    {

        public HttpResultViewModel LoadAllAppointmentList([FromBody] AppointmentFormViewModel _model)
        {
            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "LoadAllAppointmentList";
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.AppointmentModule", _dbrequest);
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




        // Method Definition

        public HttpResultViewModel LoadAppointmentDetailsById(AppointmentFormViewModel _model)
        {
            //Request Serialization
            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);

            //Object Instantion

            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();

            // Database Request Setup
            _dbrequest.Request = _request;
            _dbrequest.Type = "LoadAppointmentDetailsById";
            DatabaseConnection _conn = new DatabaseConnection();

            //Database Querry Execution
            try
            {
                ds = _conn.ExecuteDataSet("SP.AppointmentModule", _dbrequest);
                //Data Processing 
                if (ds.Tables.Count>0)
                {
                    DataTable dt = ds.Tables[0];
                    result.data = dt;
                    result.status = true;
                }
            }
            // Error Handling
            catch (Exception ex)
            {
                SystemUtilities sysutilities = new SystemUtilities();
                sysutilities.SaveError(ex);
            }

            //Response Result
            return result;
        }


        public HttpResultViewModel GetAppointmentDetailById([FromBody] AppointmentFormViewModel _model)
        {
            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "GetAppointmentDetailById";
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.AppointmentModule", _dbrequest);
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
        //Method Defination
        public HttpResultViewModel InsertAppointmentDetails (AppointmentFormViewModel _model)
        {
            DataSet ds;
            bool issucess = true;
            //Request Serialization
            string _request = JsonConvert.SerializeObject(_model);
            //Object Instantation
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "Appointment Not Inserted Sucessfully";
            DatabaseModel _dbrequest = new DatabaseModel();
            //Database Request setup
            _dbrequest.Type = "InsertAppointmentDetails";
            _dbrequest.Request = _request;
            //Database Querry Execution
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.AppointmentModule", _dbrequest);
                //Data Processing
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if(dt.Rows != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        _model.appointmentmasterid = dr["appointmentmasterid"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["appointmentmasterid"]);
                        result.data = _model;
                        result.status = true;
                        result.message = " Appointment Inserted Sucessfully";
                    }
                }

            }//Error Handling
            catch(Exception ex)
            {
                SystemUtilities sys = new SystemUtilities();
                sys.SaveError(ex);

            }               
            //Response Request
            return result;
        }

        public HttpResultViewModel RemoveAppointmentById([FromBody] AppointmentFormViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "contact has been deleted sucessfully.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "RemoveAppointmentById";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.AppointmentModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    issuccess = dr["issuccess"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["issuccess"]);
                    result.data = issuccess;
                    result.status = true;
                    result.message = " has been Deleted successfully!";
                }
            }
            return result;
        }
        public HttpResultViewModel UpdateAppointmentDetailsById([FromBody] AppointmentFormViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while saving the data! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "UpdateAppointmentDetailsById";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.AppointmentModule", _dbrequest);
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

    }
}
