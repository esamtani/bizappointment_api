using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class AppointmentFormViewModel
    {
        public string appointmentnumber { get; set; }
        public string firstname { get; set; }
        public string phoneno { get; set; }
        public string servicename { get; set; }
        public string appointmentstatus { get; set; }
        public string staffname { get; set; }
        public long userinfoid { get; set; }
        public long appointmentmasterid { get; set; }
        public long staffmasterid { get; set; }
        public long servicemasterid { get; set; }
        public long companyid { get; set; }
        public DateTime slotalloted { get; set; }
        public DateTime appointmentdate { get; set; }
        public int appointmentstatusid { get; set; }
        public string notes { get; set; }
        public bool isactive { get; set; }
        public string createdon { get; set; }
    }
}