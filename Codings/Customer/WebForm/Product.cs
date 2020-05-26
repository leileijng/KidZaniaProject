using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductDescription { get; set; }
        public string ProductQuantityConstraint { get; set; }
        public decimal OrginalPrice { get; set; }
        public decimal OriginalGST { get; set; }
        public decimal PwpPrice { get; set; }
        public decimal PwpGST { get; set; }
        public bool ProductVisibility { get; set; }
        public bool PhotoProduct { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedAt { get; set; }
    }
}