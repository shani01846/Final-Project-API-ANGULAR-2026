using System.ComponentModel.DataAnnotations;

namespace a.Dto
{
    public class RgisterDto
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(10)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "יש להזין אימייל")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }
}
