using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class WebSettingsModel
    {
        public string domain { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public decimal keydata { get; set; }
        public int websettingsid { get; set; }
    }
    public class ErrorHandlingModel
    {
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
    }

    public class HttpResultViewModel
    {
        public bool status { get; set; }
        public bool isactive { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
        public int errorCode { get; set; }
    }

    public class StandardReportModel
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class StandardReportRequest
    {
        public int reportid { get; set; }
        public string reportcode { get; set; }
        public string reportname { get; set; }
        public List<StandardReportModel> parameters { get; set; }
    }
}