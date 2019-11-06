using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Pages
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public string PageContent { get; set; }
        public string Picture { get; set; }
        public string RowNo { get; set; }
        public string Url { get; set; }
    }
}