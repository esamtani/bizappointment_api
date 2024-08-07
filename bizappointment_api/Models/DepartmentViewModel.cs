using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class DepartmentViewModel
    {
        public string departmentcode { get; set; }
        public string departmentname { get; set; }
        public string phoneno { get; set; }
        public long departmentid { get; set; }
        public long companyid { get; set; }
        public bool isactive { get; set; }
        public string createdon { get; set; }
    }
}