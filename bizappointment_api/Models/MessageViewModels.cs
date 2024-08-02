using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class MessageViewModels
    {
    }
    public class GupShupMessage
    {
        public string type { get; set; }
        public string text { get; set; }
        //public string isHSM { get; set; }
        public string phoneno { get; set; }

    }
    public class SendMessageViewModel
    {
        public string phoneno { get; set; }
        public string methodcode { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public long logininfoid { get; set; }
    }


    public class WhatsappParamViewModel
    {
        public string paramcode { get; set; }
        public string parametername { get; set; }
        public bool isstatic { get; set; }
        public string staticvalue { get; set; }

    }
}