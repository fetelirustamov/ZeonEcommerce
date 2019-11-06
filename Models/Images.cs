using System;
using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Images
    {
        [Key]
        public int ImagesId { get; set; }
        public string HomeSlider { get; set; }
        public string ProductSlider { get; set; }
        public int RowNo { get; set; }

        public Nullable<int> ProductsID { get; set; }
        public virtual Products Products { get; set; }


    }
}