using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bizappointment_api.Models
{
    public class PurchaseViewModel
    {
       
    }
    public class PurchaseMasterViewModel
    {
        public long transactionmasterid { get; set; }
        public float transactionamount { get; set; }
        public long purchasemasterid { get; set; }
        public string purchaseno { get; set; }
        public DateTime purchasedate { get; set; }
        public long logininfoid { get; set; }
        public float totalpurchaseamount { get; set; }
        public int purchasestatusid { get; set; }
        public string purchasestatus { get; set; }
        public string fullname { get; set; }
        public string phoneno { get; set; }
        public string emailid { get; set; }
        public string orderid { get; set; }

    }
    public class RazorpayPaymentMasterViewModel
    {
        public long razorpaypaymentmasterid { get; set; }
        public string paymentno { get; set; }
        public string codeformessage { get; set; }
        public string transactionno { get; set; }
        public long invoicemasterid { get; set; }
        public DateTime paymentdate { get; set; }
        public long transactionmasterid { get; set; }
        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
    }

    public class PaymentMasterViewModel
    {
        public long paymentmasterid { get; set; }
        public long ordermasterid { get; set; }
        public long invoicemasterid { get; set; }
        public long transactionmasterid { get; set; }
        public int companyid { get; set; }
        public long userinfoid { get; set; }
        public int paymentmethodid { get; set; }
        public int quantity { get; set; }
        public string paymentno { get; set; }
        public string token { get; set; }
        public string responsestatus { get; set; }
        public string responsemsg { get; set; }
        public float totalamount { get; set; }
        public DateTime paymentdate { get; set; }
        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
        public string transactionno { get; set; }
        public string razorpay_payment_link_id { get; set; }
        public string razorpay_payment_link_reference_id { get; set; }
        public string razorpay_payment_link_status { get; set; }

    }

    public class RazorpayPaymentForTherapyMasterViewModel
    {
        public long razorpaypaymentmasterid { get; set; }
        public string paymentno { get; set; }
        public string codeformessage { get; set; }
        public string transactionno { get; set; }
        public long invoicemasterid { get; set; }
        public DateTime paymentdate { get; set; }
        public long transactionmasterid { get; set; }
        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
        public long slotavailabilityid { get; set; }
        public long usercoursepackagemasterid { get; set; }
        public long coursepackagemasterid { get; set; }
        public long vendorinfoid { get; set; }
        public int packagetypeid { get; set; }
        public string additionalnote { get; set; }
        public long coursepackagedetailsid { get; set; }
    }
    public class InvoiceDetailsViewModel
    {
        public long invoicedetails { get; set; }
        public long invoicemasterid { get; set; }
        public long coursemasterid { get; set; }
        public float courseamount { get; set; }
        public float discountamount { get; set; }
    }
}