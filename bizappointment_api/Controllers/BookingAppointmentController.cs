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
    public class BookingAppointmentController : ApiController
    {

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
        public HttpResultViewModel InsertAppointmentDetails([FromBody] AppointmentFormViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;

            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while adding the maze! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "InsertAppointmentDetails";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.AppointmentModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    //_model.contactformdetailsid = dr["contactformdetailsid"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["contactformdetailsid"]);
                    result.data = _model;
                    result.status = true;
                    result.message = "You have successfully added new request! ";
                }
            }
            return result;
        }

    }
}
