using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Advertising
    {
        [Key]
        public int AdvertisingId { get; set; }
        public int RowNo { get; set; }
        public string Picture { get; set; }
    }
}