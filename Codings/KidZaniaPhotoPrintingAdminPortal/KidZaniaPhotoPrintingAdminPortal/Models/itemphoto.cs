//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KidZaniaPhotoPrintingAdminPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class itemphoto
    {
        public string itemphoto_id { get; set; }
        public string photo { get; set; }
        public string p_id { get; set; }
        public string lineitem_id { get; set; }
        public string assigned_printer_id { get; set; }
        public string printing_status { get; set; }
        public System.DateTime updated_at { get; set; }
    
        public virtual lineitem lineitem { get; set; }
        public virtual printer printer { get; set; }
        public virtual order order { get; set; }
    }
}
