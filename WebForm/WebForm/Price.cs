using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm
{
    public class Price
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Unit { get; set; }
        public decimal UnitGST { get; set; }
    }
}