using a.Models;
using System.ComponentModel.DataAnnotations;

namespace NET.Models
{
    public class Present
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "יש להזין שם")]
        [MaxLength(60)]
        public string Name { get; set; }= string.Empty;

        [MaxLength(180)]
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;

        [Range(10, int.MaxValue, ErrorMessage = " הערך מינימלי למתנה הוא ־10")]
        public int Price { get; set; }

        [Required(ErrorMessage = "יש להזין מזהה קטגוריה")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "יש להזין מזהה תורם")]
        public int DonorId { get; set; }
        public Donor Donor { get; set; }
        public bool IsLotteryDone { get; set; } = false;
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();


    }
}
