using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Settings
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(50)]
        public string Phone { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Fax { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Google { get; set; }
        public string Twitter { get; set; }
    }
}