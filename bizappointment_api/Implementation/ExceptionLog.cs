using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using bizappointment_api.Models;
namespace bizappointment_api.Implementation
{
    public class ExceptionLog
    {
        public void LogError(ExceptionViewModel _model)
        {

            DataSet ds;

            string _request = JsonConvert.SerializeObject(_model);
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "AppError";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.ErrorHandlingModule", _dbrequest);

        }
    }
}