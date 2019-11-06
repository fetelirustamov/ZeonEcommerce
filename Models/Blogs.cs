using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeonEcommerce.Models
{
    public class Blogs
    {
        [Key]
        public int BlogsId { get; set; }
        [Required]
        public string Title { get; set; }
        public string BlogContent { get; set; }
        public DateTime CreateTime { get; set; }
        public string Picture { get; set; }


        public int SuppliersID  { get; set; }
        public virtual Suppliers Suppliers { get; set; }

        public virtual ICollection<BlogComments> BlogComments { get; set; }
    }
}