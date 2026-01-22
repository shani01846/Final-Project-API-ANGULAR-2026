using a.Dto;
using a.Interfaces;
using a.Models;
using a.Repositories;
using NET.Models;
using System.Drawing;

namespace a.Services
{
    public class LotteryResultService : ILotteryResultService
    {
        private readonly ILotteryResultRepository _repository;
        private readonly ILogger<LotteryResultService> _logger;

        public LotteryResultService(ILogger<LotteryResultService> logger,ILotteryResultRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<LotteryResultDto> createWinner(CreateLotteryResultDto lr)
        {

            var createdWinner = new LotteryResult
            {
                WinnerUserId = lr.WinnerUserId,
                PresentId = lr.PresentId,
                LotteryDate = lr.LotteryDate
            };


            var craetedWinner = await _repository.createWinner(createdWinner);
            return MapToResponseDto(craetedWinner);
        }

        public async Task<IEnumerable<LotteryResultDto>> GetAll()
        {
            var results = await _repository.GetAll();
            return results.Select(MapToResponseDto);
        }

        public async Task<LotteryResultDto> makeLottery(int presentId)
        {
            var result = await _repository.makeLottery(presentId)
            return MapToResponseDto(result);
        }
        private static LotteryResultDto MapToResponseDto(LotteryResult lr)
        {
            return new LotteryResultDto
            {
                Id = lr.Id,
                LotteryDate = lr.LotteryDate,
                PresentDescription = lr.Present.Description,
                Winner = lr.Winner
            };
        }
    }
}
