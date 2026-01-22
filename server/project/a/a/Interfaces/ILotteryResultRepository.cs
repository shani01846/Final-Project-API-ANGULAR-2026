using a.Models;
using NET.Models;

namespace a.Interfaces
{
    public interface ILotteryResultRepository
    {
        Task<LotteryResult> createWinner(LotteryResult lr); //(send email to the winner)
       Task<IEnumerable<LotteryResult>> GetAll(); //includes winner and present
        Task<LotteryResult> makeLottery(int presentId);//– update Lottery Done

    }
}
