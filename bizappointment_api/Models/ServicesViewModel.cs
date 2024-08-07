using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class ServicesViewModel
    {
        public string servicenumber { get; set; }
        public long servicemasterid { get; set; }
        public string servicename { get; set; }
        public long  serviceprice { get; set; }
        public string department { get; set; }
        public string description { get; set; }
        public bool isactive { get; set; }
        public string createdon { get; set; }
    }
}