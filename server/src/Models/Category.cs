using System.ComponentModel.DataAnnotations;

namespace NET.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "יש להזין שם")]
        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Present> Presents { get; set; } = new List<Present>();
    }
}