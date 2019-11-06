using System;
using System.Collections.Generic;

namespace ZeonEcommerce.Models
{
    public class Orders
    {
        public int OrdersId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderAdress { get; set; }
        public string OrderPhone { get; set; }
        public string OrderName { get; set; }
        public string TotalPrice { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}