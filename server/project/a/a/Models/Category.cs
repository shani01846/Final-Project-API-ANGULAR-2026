using System.ComponentModel.DataAnnotations;

namespace NET.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Present> Presents { get; set; } = new List<Present>();
    }
}