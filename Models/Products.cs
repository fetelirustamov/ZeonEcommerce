using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeonEcommerce.Models
{
    public class Products
    {
        [Key]
        public int ProductsId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Discont { get; set; }
        public string Picture { get; set; }
        public int Stock { get; set; }
        public DateTime CreateTime { get; set; }


        public int SuppliersID { get; set; }
        public virtual Suppliers Suppliers { get; set; }

        public int BrandsID { get; set; }
        public virtual Brands Brands { get; set; }

    
        public virtual  ICollection<Basket> Basket { get; set; }

        public virtual ICollection<Images> Images { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public int CategoriesID { get; set; }
        public virtual Categories Categories { get; set; }

        public virtual ICollection<ProductsComments> ProductsComments { get; set; }
        public virtual ICollection<ProductParameters> ProductParameters { get; set; }

    }
}