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
    public class CartController : ApiController
    {

        public HttpResultViewModel LoadCartDetailsById([FromBody] AppointmentFormViewModel _model)
        {

            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "LoadCartDetailsById";
            DatabaseConnection _conn = new DatabaseConnection();
            try
            {
                ds = _conn.ExecuteDataSet("SP.CartModule", _dbrequest);
                if (ds.Tables.Count > 0)
                {
                    result.data = ds;
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
        public HttpResultViewModel RemoveFromCart([FromBody] AppointmentFormViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;

            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while removing the course! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "RemoveFromCart";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.CartModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    issuccess = dr["issuccess"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["issuccess"]);
                    result.data = issuccess;
                    result.status = true;
                    result.message = "You have successfully removed course! ";
                }
            }
            return result;
        }

    }
}
