using a.Dto;
using NET.Models;
using System.ComponentModel.DataAnnotations;

namespace a.Models
{
    public class LotteryResult
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "יש להזין מזהה מתנה")]
        public int PresentId { get; set; }
        public Present Present { get; set; }

        [Required(ErrorMessage = "יש להזין מזהה זוכה")]
        public int WinnerUserId { get; set; }
        public User Winner { get; set; }
        public DateTime LotteryDate { get; set; }
    }
}