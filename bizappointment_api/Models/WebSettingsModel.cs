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
        public float keydata { get; set; }
        public int websettingsid { get; set; }
    }
    public class WebSettingsRequestModel
    {
        public string domain { get; set; }
    }
}