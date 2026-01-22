using a.Dto;
using a.Models;
using NET.Models;

namespace a.Interfaces
{
    public interface ILotteryResultService
    {
        Task<LotteryResultDto> createWinner(CreateLotteryResultDto lr); //(send email to the winner)
       Task<IEnumerable<LotteryResultDto>> GetAll(); //includes winner and present
        Task<LotteryResultDto> makeLottery(int presentId);//– update Lottery Done
    }
}
