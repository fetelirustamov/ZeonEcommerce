using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Brands
    {
        [Key]
        public int BrandsId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}