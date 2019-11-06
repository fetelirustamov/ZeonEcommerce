using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeonEcommerce.Models
{ 
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }

        public int ProductsID { get; set; }
        public virtual Products Products { get; set; }

        public int OrdersID { get; set; }
        public virtual Orders Orders { get; set; }
    }
}