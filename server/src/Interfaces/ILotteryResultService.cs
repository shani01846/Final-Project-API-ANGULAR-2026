using a.Dto;
using a.Models;
using NET.Models;

namespace a.Interfaces
{
    public interface ILotteryResultService
    {
        Task<LotteryResultDto> CreateWinnerAsync(CreateLotteryResultDto lr); //(send email to the winner)
       Task<IEnumerable<LotteryResultDto>> GetAllAsync(); //includes winner and present
        Task<LotteryResultDto> MakeLotteryAsync(int presentId);//– update Lottery Done
    }
}
