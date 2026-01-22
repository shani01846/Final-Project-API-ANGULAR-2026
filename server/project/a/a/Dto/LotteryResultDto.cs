using NET.Models;

namespace a.Dto
{
    public class LotteryResultDto
    {
        public int Id { get; set; }
        public int PresentId { get; set; }
        public Present Present { get; set; }
        public string PresentDescription { get; set; }
        public string PresentPrice { get; set; }
        public User Winner { get; set; }
        public DateTime LotteryDate { get; set; }
    }
    public class CreateLotteryResultDto
    {
        public int PresentId { get; set; }
        public int WinnerUserId { get; set; }
        public DateTime LotteryDate { get; set; }
    }
}
