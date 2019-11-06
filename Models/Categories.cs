using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Categories
    {
        [Key]
        public int CategoriesId  { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubCategoryID { get; set; }


        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Logs> Logs { get; set; }
        public virtual ICollection<ParameterType> ParameterType { get; set; }
    }
}