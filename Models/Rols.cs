using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Rols
    {
        [Key]
        public int RolsId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual ICollection<Suppliers> Suppliers { get; set; }
    }
}