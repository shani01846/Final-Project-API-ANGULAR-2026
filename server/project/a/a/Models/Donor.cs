using NET.Models;
namespace a.Models
{
    public class Donor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Present> Presents { get; set; }
    }
}