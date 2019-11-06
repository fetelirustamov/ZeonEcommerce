using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeonEcommerce.Models
{
    public class Basket
    {
        [Key]
        public int BasketId { get; set; }
        public int Quantity { get; set; }

        public int CustomersID { get; set; }
        public virtual Customers Customers { get; set; }

        public int ProductsID { get; set; }
        public virtual Products Products { get; set; }
    }
}