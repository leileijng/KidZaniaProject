//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomerPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class payment_status
    {
        public string mid { get; set; }
        public string transaction_id { get; set; }
        public string order_id { get; set; }
        public string acquirer_transaction_id { get; set; }
        public Nullable<float> request_amount { get; set; }
        public string request_ccy { get; set; }
        public Nullable<float> authorized_amount { get; set; }
        public string authorized_ccy { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }
        public string acquirer_response_code { get; set; }
        public string acquirer_response_msg { get; set; }
        public string acquirer_authorization_code { get; set; }
        public string created_timestamp { get; set; }
        public string acquirer_created_timestamp { get; set; }
        public string request_timestamp { get; set; }
        public string request_mid { get; set; }
        public string transaction_type { get; set; }
    }
}