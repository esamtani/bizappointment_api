using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class StaffViewModel
    {
        public string staffnumber { get; set; }
        public long staffmasterid { get; set; }
        public string firstname { get; set; }
        public string contactnumber { get; set; }
        public string lastname { get; set; }
        public long servicemasterid { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string address { get; set; }
        public bool isactive { get; set; }
        public string createdon { get; set; }
    }
}