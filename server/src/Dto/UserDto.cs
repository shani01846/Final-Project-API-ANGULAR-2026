using NET.Models;
using System.ComponentModel.DataAnnotations;

namespace a.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "לפחות 6 תווים")]
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
    public class CreateUserDto
    {
        [Required(ErrorMessage = "יש להזין אימייל")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "לפחות 6 תווים")]
        public string Password { get; set; }
        [Required(ErrorMessage = "יש להזין שם פרטי")]
        [MaxLength(60)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "יש להזין שם משפחה")]
        [MaxLength(60)]
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "יש להזין כתובת ")]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

    }

    public class UpdateUseDto
    {  public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsManager { get; set; } = false;

    }
}
