using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using bizappointment_api.Models;
using bizappointment_api.Implementation;

namespace bizappointment_api.Controllers
{
    public class WebSettingsController : ApiController
    {
        public HttpResultViewModel WebsettingList([FromBody] WebSettingsModel _model)
        {

            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "GetWebSettingsValueList";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.WebSettingsModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                result.data = dt;
                result.status = true;
            }
            return result;
        }
    }
}
