using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeonEcommerce.Models
{
    public class ProductsComments
    {
        [Key]
        public int ProductCommentsId { get; set; }
        [Required]
        public string Comment { get; set; }
        public DateTime CreateTime { get; set; }

        public int? CustomersID { get; set; }
        public virtual Customers Customers { get; set; }

        public int? SuppliersID { get; set; }
        public virtual Suppliers Suppliers { get; set; }

        public int ProductsID { get; set; }
        public virtual Products Products { get; set; }
    }
}