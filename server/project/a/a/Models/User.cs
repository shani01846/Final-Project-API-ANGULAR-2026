using System.ComponentModel.DataAnnotations;

namespace NET.Models
{
    
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public bool IsManager { get; set; } = false;
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    }
}
