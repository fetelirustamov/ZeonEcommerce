using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeonEcommerce.Models
{
    public class BlogComments
    {
        [Key]
        public int BlogsCommentsId { get; set; }
        public string BlogComment { get; set; }
        public DateTime CreateTime { get; set; }


        public int BlogsID { get; set; }
        public virtual Blogs Blogs { get; set; }

        public int? CustomersID { get; set; }
        [ForeignKey("CustomersID")]
        public virtual Customers Customers { get; set; }

        public int? SuppliersID { get; set; }
        [ForeignKey("SuppliersID")]
        public virtual Suppliers Suppliers { get; set; }
    }
}