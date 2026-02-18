using NET.Models;
using System.ComponentModel.DataAnnotations;
namespace a.Models
{
    public class Donor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "יש להזין שם")]
        [MaxLength(60)]
        public string Name { get; set; }
        [Required(ErrorMessage = "יש להזין אימייל")]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Present> Presents { get; set; }
    }
}