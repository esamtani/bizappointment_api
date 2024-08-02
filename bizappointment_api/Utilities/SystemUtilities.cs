using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using bizappointment_api.Implementation;
using bizappointment_api.Models;
using bizappointment_api.Utilities;

namespace bizappointment_api.Utilities
{
    public class SystemUtilities
    {
        private static Random random = new Random();
        public static long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ReplaceString(string fulltext, string oldtext, string newtext)
        {
            return fulltext.Replace(oldtext, newtext);
        }

        public string calculateRFC2104HMAC(string data, string secret)
        {
            byte[] key = Encoding.ASCII.GetBytes(secret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            return myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
        public void SaveError(Exception ex)
        {
            DataSet ds;
            ErrorViewModel er = new ErrorViewModel();
            er.Source = ex.Source;
            er.StackTrace = ex.StackTrace;
            er.Message = ex.Message;
            string _request = JsonConvert.SerializeObject(er);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "AddError";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.ErrorLogModule", _dbrequest);
        }

    }

}