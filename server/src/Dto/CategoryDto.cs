using System.ComponentModel.DataAnnotations;

namespace a.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "יש להזין שם")]
        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;
    }
}
