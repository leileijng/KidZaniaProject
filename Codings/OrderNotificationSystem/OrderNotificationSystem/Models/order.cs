//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderNotificationSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        public string order_id { get; set; }
        public string pid { get; set; }
        public decimal total_amount { get; set; }
        public string status { get; set; }
        public System.DateTime updatedAt { get; set; }
    }
}