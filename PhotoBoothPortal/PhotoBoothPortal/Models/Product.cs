using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoBoothPortal.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //The instructor view functionality logic will check this property to decide
        //whether to display the lesson type to the user (instructor role user) to choose.
        public decimal ProductPrice { get; set; }
        public decimal ProductGST { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
    }
}