using NET.Models;

namespace a.Models
{
    public class LotteryResult
    {
        public int Id { get; set; }
        public int PresentId { get; set; }
        public Present Present { get; set; }
        public int WinnerUserId { get; set; }
        public User Winner { get; set; }
        public DateTime LotteryDate { get; set; }
    }
}