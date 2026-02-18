using System.ComponentModel.DataAnnotations;

namespace NET.Models
{
    
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "יש להזין אימייל")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "לפחות 6 תווים")]
        public string Password { get; set; }

        [Required(ErrorMessage = "יש להזין שם פרטי")]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "יש להזין שם משפחה")]
        [MaxLength(60)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "יש להזין מספר פלאפון ")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "יש להזין כתובת ")]
        [MaxLength(100)]
        public string Address {  get; set; } = string.Empty;
        public bool IsManager { get; set; } = false;
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    }
}
