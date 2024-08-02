using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using bizappointment_api.Implementation;
using bizappointment_api.Models;
using bizappointment_api.Utilities;
using Newtonsoft.Json;
namespace bizappointment_api.Controllers
{
    public class PurchaseController : ApiController
    {

        string _razorpayKey = "";
        string _razorpaySecret = "";
        PurchaseController()
        {
            string _server = WebConfigurationManager.AppSettings["DeploymentServer"];
            if (_server == "prod")
            {
                _razorpayKey = "rzp_live_Ij6KpJb8wXn3Mj:TGoQLZmOvd0QyfmyF7V9gCRF";
                _razorpaySecret = "TGoQLZmOvd0QyfmyF7V9gCRF";
            }
            else if (_server == "uat")
            {
                _razorpayKey = "rzp_test_n2Qfgk7G6EiiZt:IBjoMNAoAdkYShLmIoXhu8QD";
                _razorpaySecret = "IBjoMNAoAdkYShLmIoXhu8QD";
            }
            else if (_server == "qa")
            {
                _razorpayKey = "rzp_test_n2Qfgk7G6EiiZt:IBjoMNAoAdkYShLmIoXhu8QD";
                _razorpaySecret = "IBjoMNAoAdkYShLmIoXhu8QD";
            }
            else if (_server == "dev")
            {
                _razorpayKey = "rzp_test_n2Qfgk7G6EiiZt:IBjoMNAoAdkYShLmIoXhu8QD";
                _razorpaySecret = "IBjoMNAoAdkYShLmIoXhu8QD";
            }
        }
        // to add payment data in purchase & transaction -- payment status as created
        public async Task<HttpResultViewModel> AddPurchaseDetails([FromBody] PurchaseMasterViewModel _model)
        {
            DataSet ds;
            bool issuccess = false;

            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "There was an error while adding the purchase! Please try again.";
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "AddPurchaseDetails";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.PaymentModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    _model.transactionmasterid = dr["transactionmasterid"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr["transactionmasterid"]);
                    _model.transactionamount = dr["transactionamount"].Equals(DBNull.Value) ? 0 : float.Parse(dr["transactionamount"].ToString());
                    _model.purchaseno = dr["purchaseno"].Equals(DBNull.Value) ? "" : dr["purchaseno"].ToString();
                }
            }
            string _requestt = "";

            dynamic obj = new ExpandoObject();
            obj.amount = _model.transactionamount;
            obj.currency = "INR";
            obj.receipt = _model.transactionmasterid.ToString();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.razorpay.com/v1/orders"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(_razorpayKey));
                    request.Headers.TryAddWithoutValidation("Authorization", $" Basic {base64authorization}");

                    request.Content = new StringContent(JsonConvert.SerializeObject(obj));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    httpClient.DefaultRequestHeaders.ConnectionClose = true;
                    var response = await httpClient.SendAsync(request);

                    var content = await response.Content.ReadAsAsync<dynamic>();
                    _requestt = JsonConvert.SerializeObject(content);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        result.data = null;
                        result.status = false;
                        result.message = "There was a problem accessing the web resource.";
                    }
                    else
                    {
                        _dbrequest.Request = _requestt;
                        _dbrequest.Type = "AddOrderIdInTransactionMaster";
                        _conn = new DatabaseConnection();
                        ds = _conn.ExecuteDataSet("SP.PaymentModule", _dbrequest);
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                issuccess = dr["issuccess"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["issuccess"]);
                                _model.orderid = dr["order_id"].Equals(DBNull.Value) ? "" : dr["order_id"].ToString();
                                _model.fullname = dr["fullname"].Equals(DBNull.Value) ? "" : dr["fullname"].ToString();
                                _model.phoneno = dr["phoneno"].Equals(DBNull.Value) ? "" : dr["phoneno"].ToString();
                                _model.emailid = dr["emailid"].Equals(DBNull.Value) ? "" : dr["emailid"].ToString();
                                result.data = _model;
                                result.status = true;
                                result.message = "You have successfully added new transaction! ";
                            }
                        }

                    }
                }
            }

            return result;
        }


        // to verify and mark payment as paid -- after payment it will send the message on whatsapp
        public HttpResultViewModel CreatePaymentForPurchase([FromBody] RazorpayPaymentMasterViewModel _model)
        {
            DataSet ds;

            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            result.message = "We are checking your payment. Please contact support if the payment went through.";

            SystemUtilities _util = new SystemUtilities();
            var generatedSignature = _util.calculateRFC2104HMAC(_model.razorpay_order_id + "|" + _model.razorpay_payment_id, _razorpaySecret);

            if (!string.IsNullOrEmpty(_model.razorpay_signature) && generatedSignature == _model.razorpay_signature)
            {

                DatabaseModel _dbrequest = new DatabaseModel();
                _dbrequest.Request = _request;
                _dbrequest.Type = "CreatePaymentForPurchase";
                DatabaseConnection _conn = new DatabaseConnection();
                ds = _conn.ExecuteDataSet("SP.PaymentModule", _dbrequest);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        _model.razorpaypaymentmasterid = dr["razorpaypaymentmasterid"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr["razorpaypaymentmasterid"]);
                        _model.paymentno = dr["paymentno"].Equals(DBNull.Value) ? "" : dr["paymentno"].ToString();
                        _model.transactionno = dr["transactionno"].Equals(DBNull.Value) ? "" : dr["transactionno"].ToString();
                        _model.razorpay_order_id = dr["razorpay_order_id"].Equals(DBNull.Value) ? "" : dr["razorpay_order_id"].ToString();
                        _model.invoicemasterid = dr["invoicemasterid"].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr["invoicemasterid"]);
                        //_model.codeformessage = dr["codeformessage"].Equals(DBNull.Value) ? "" : dr["codeformessage"].ToString();
                        var phoneno = dr["phoneno"].Equals(DBNull.Value) ? "" : dr["phoneno"].ToString();

                        result.data = _model;
                        result.status = true;
                        result.message = "You have successfully added new payment! ";

                        DataTable _messageTable = ds.Tables[1];
                        if (_messageTable.Rows.Count > 0)
                        {
                            WhatsappSend sendmsg = new WhatsappSend();
                            foreach (DataRow dr2 in _messageTable.Rows)
                            {
                                var whatsappmessage = dr2["msgtext"].Equals(DBNull.Value) ? "" : dr2["msgtext"].ToString();

                                if (!string.IsNullOrEmpty(phoneno))
                                {
                                    sendmsg.SendMessage(phoneno, whatsappmessage);
                                }
                                else
                                {
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        // to load invoice details
        public HttpResultViewModel LoadInvoiceDetailsById([FromBody] InvoiceDetailsViewModel _model)
        {

            DataSet ds;
            string _request = JsonConvert.SerializeObject(_model);
            HttpResultViewModel result = new HttpResultViewModel();
            DatabaseModel _dbrequest = new DatabaseModel();
            _dbrequest.Request = _request;
            _dbrequest.Type = "LoadInvoiceDetailsById";
            DatabaseConnection _conn = new DatabaseConnection();
            ds = _conn.ExecuteDataSet("SP.InvoiceModule", _dbrequest);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows != null && dt.Rows.Count > 0)
                {
                    result.data = ds;
                    result.status = true;
                    result.message = "The invoice details has been loaded successfully!";

                }
            }
            return result;
        }
    }
}
