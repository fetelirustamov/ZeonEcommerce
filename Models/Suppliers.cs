using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeonEcommerce.Models
{
    public class Suppliers
    {
        [Key]
        public int SuppliersId { get; set; }
        
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string SurName { get; set; }
        [Required, MaxLength(50)]
        public string Company { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string Street { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public Guid Token { get; set; }
        public bool IsActive { get; set; }

        public int RolsID { get; set; }
        public virtual Rols Rols { get; set; }
        
        public virtual ICollection<ProductsComments> ProductsComments { get; set; }
        public virtual ICollection<BlogComments> BlogComments { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Blogs> Blogs { get; set; }

    }
}