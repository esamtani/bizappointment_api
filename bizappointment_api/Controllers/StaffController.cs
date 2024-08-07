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
        public HttpResultViewModel InsertStaffDetails([FromBody] StaffViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;

            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while adding the Department Name! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "InsertStaffDetails";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.StaffModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    result.data = _model;
                    result.status = true;
                    result.message = "You have successfully added new Department name! ";
                }
            }
            return result;
        }
        public HttpResultViewModel GetStaffDetailById([FromBody] StaffViewModel _model)
        {
            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "GetStaffDetailById";
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
    }
}
