using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class DatabaseModel
    {
        public string Type { get; set; }
        public string Request { get; set; }
        public int LoggedinId { get; set; }
    }
}