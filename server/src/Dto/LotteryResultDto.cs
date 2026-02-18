using NET.Models;
using System.ComponentModel.DataAnnotations;

namespace a.Dto
{
    public class LotteryResultDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "יש להזין מזהה מתנה")]
        public int PresentId { get; set; }
        public Present Present { get; set; }
        public string PresentDescription { get; set; }
        public string PresentPrice { get; set; }

        [Required]
        public UserDto Winner { get; set; }
        public DateTime LotteryDate { get; set; }
    }
    public class CreateLotteryResultDto
    {
        [Required(ErrorMessage = "יש להזין מזהה מתנה")]
        public int PresentId { get; set; }

        [Required(ErrorMessage = "יש להזין מזהה זוכה")]
        public int WinnerUserId { get; set; }
        public DateTime LotteryDate { get; set; }
    }
}
