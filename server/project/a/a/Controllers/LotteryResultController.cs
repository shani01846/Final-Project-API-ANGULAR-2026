using a.Dto;
using a.Interfaces;
using a.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace a.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryResultController : ControllerBase
    {
        private readonly ILotteryResultService _lotteryResultService;
        private readonly ILogger<LotteryResultController> _logger;
        public LotteryResultController(ILotteryResultService lotteryResultService,ILogger<LotteryResultController> logger)
        {
            _logger = logger;
            _lotteryResultService = lotteryResultService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonorDto>>> GetAll()
        {
            var donors = await _lotteryResultService.GetAllAsync();
            return Ok(donors);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateWinner([FromBody] CreateLotteryResultDto winner )
        {
            var winnerRes =_lotteryResultService.CreateWinnerAsync(winner);
            //*******
            //send email to the winner
            //*******
            return Ok(winnerRes);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> MakeLottery(int id)
        {
            var winnerRes = _lotteryResultService.MakeLotteryAsync(id);
            //*******
            //send email to the winner
            //*******
            return Ok(winnerRes);
        }

    }
}
