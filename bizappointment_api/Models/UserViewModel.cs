using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class UserViewModel
    {
    }
    public class ErrorViewModel
    {
        public string ErrorCode { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string InnerException { get; set; }
        public string HResult { get; set; }
        public string Source { get; set; }
        public int State { get; set; }
        public string Server { get; set; }
        public string Procedure { get; set; }
        public string Errors { get; set; }
        public string ClientConnectionid { get; set; }

    }
    public class LoginViewModel
    {
        public string password { get; set; }
        public string username { get; set; }
        public long userinfoid { get; set; }
        public long logininfoid { get; set; }
        public bool isexist { get; set; }
        public string userrolecode { get; set; }
        public string userrolename { get; set; }
        public int userroleid { get; set; }
        public string token { get; set; }
        public bool istemppassword { get; set; }
        public string emailaddress { get; set; }

        public int errorCode { get; set; }

    }
    public class RegistrationViewModel
    {
        public string userrolecode { get; set; }
        public string phoneno { get; set; }
        public string emailid { get; set; }
        public string displayname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public bool isagreed { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public bool isexist { get; set; }
        public long logininfoid { get; set; }
        public long userinfoid { get; set; }

    }
    public class PasswordChangeViewModel
    {
        public string oldpassword { get; set; }
        public string password { get; set; }
        public long logininfoid { get; set; }
    }
}