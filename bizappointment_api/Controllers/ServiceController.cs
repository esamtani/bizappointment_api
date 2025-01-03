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
    public class ServiceController : ApiController
    {
        //Method Declaration
        public HttpResultViewModel AddNewService(ServicesViewModel _model)
        {
            DataSet ds;
            bool issucess = false;
            //Request Serailazation
            string _request = JsonConvert.SerializeObject(_model);
            //Object instantation
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "Data not Inserted Properly please Try again Later";
            DatabaseModel _dbrequest = new DatabaseModel();

            //Database Request setup
            _dbrequest.Type = "AddNewService";
            _dbrequest.Request = _request;
            //Database Querry Execution 
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.ServiceModule", _dbrequest);
                //Data Processing 
                if (ds.Tables.Count>0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows != null && dt.Rows.Count>0)
                    {
                        DataRow dr = dt.Rows[0];
                        _model.servicemasterid = dr["servicemasterid"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["servicemasterid"]);
                        result.data = _model;
                        result.status = true;
                        result.message = "Sucessfully Inserted New Service";
                    }

                }

            }    //Error Handling
            catch (Exception ex)
            {
                SystemUtilities sys = new SystemUtilities();
                sys.SaveError(ex);

            }

            //Return Result
            return result;
        }







        //Method Declaration
        public HttpResultViewModel GetServiceDetailsById(ServicesViewModel _model)
        {
            DataSet ds;
            // Request Serialization
            string _request = JsonConvert.SerializeObject(_model);

            //Object instantion
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();

            //Database Request Setup
            _dbrequest.Request = _request;
            _dbrequest.Type = "GetServiceDetailsById";

            //Database Querry Execution
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.ServiceModule", _dbrequest);
                //Data Processing
                if (ds.Tables.Count>0)
                {
                    DataTable dt = ds.Tables[0];
                    result.data = dt;
                    result.status = true;
                }

            }
            //Error Handling
            catch (Exception ex)
            {
                SystemUtilities _sysutli = new SystemUtilities();
                _sysutli.SaveError(ex);
            }
            
            
            //Return Result
            return result;
        }












        public HttpResultViewModel EditServicesDetails([FromBody] ServicesViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while saving the data! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "EditServicesDetails";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.ServiceModule", _dbrequest);
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
        public HttpResultViewModel RemoveServicesById([FromBody] ServicesViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "Department name has been deleted sucessfully.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "RemoveServicesById";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.ServiceModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    issuccess = dr["issuccess"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["issuccess"]);
                    result.data = issuccess;
                    result.status = true;
                    result.message = "Service name has been Deleted successfully!";
                }
            }
            return result;
        }

        public HttpResultViewModel LoadAllStaffServicesList([FromBody] ServicesViewModel _model)
        {
            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "LoadAllStaffServicesList";
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

    }
}
