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
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.lineitems = new HashSet<lineitem>();
        }
    
        public string product_id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public decimal original_price { get; set; }
        public decimal original_GST { get; set; }
        public Nullable<decimal> pwp_price { get; set; }
        public Nullable<decimal> pwp_GST { get; set; }
        public string quantity_constraint { get; set; }
        public bool visibility { get; set; }
        public bool photo_product { get; set; }
        public string updated_by { get; set; }
        public System.DateTime updated_at { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lineitem> lineitems { get; set; }
        public virtual staff staff { get; set; }
    }
}
