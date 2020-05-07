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
        public decimal ProductPrice { get; set; }
        public decimal ProductGST { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
    }
}