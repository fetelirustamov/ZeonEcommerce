using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Customers
    {
        [Key]
        public int CustomersId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }
        public Guid Token { get; set; }
        public virtual ICollection<Basket> Basket { get; set; }
        public virtual ICollection<BlogComments> BlogComments { get; set; }
        public virtual ICollection<ProductsComments> ProductsComments { get; set; }
        public virtual ICollection<Logs> Logs { get; set; }
    }
}