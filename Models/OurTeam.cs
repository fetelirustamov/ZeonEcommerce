using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class OurTeam
    {
        [Key]
        public int OurTeamId { get; set; }
        [Required, MaxLength(50)]
        public string  Name { get; set; }
        [Required, MaxLength(100)]
        public string  Job { get; set; }
        public string  Facebook { get; set; }
        public string  Twitter { get; set; }
        public string  Linkedin { get; set; }
        public string  Google { get; set; }
        public string  About { get; set; }
        public string  Picture { get; set; }
    }
}