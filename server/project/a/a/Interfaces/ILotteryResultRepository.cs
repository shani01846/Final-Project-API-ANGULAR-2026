using a.Models;
using NET.Models;

namespace a.Interfaces
{
    public interface ILotteryResultRepository
    {
        Task<LotteryResult> CreateWinnerAsync(LotteryResult lr); //(send email to the winner)
       Task<IEnumerable<LotteryResult>> GetAllAsync(); //includes winner and present
        Task<LotteryResult> MakeLotteryAsync(int presentId);//– update Lottery Done

    }
}
