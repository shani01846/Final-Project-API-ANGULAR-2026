using a.Dto;
using a.Interfaces;
using a.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreApi.DTOs;
using StoreApi.Interfaces;

namespace a.Controllers
{
    //Task<IEnumerable<DonorDto>> GetAllAsync();
    //Task<DonorDto> UpdateAsync(UpdateDonorDto donor);
    //Task<bool> DeleteAsync(int id);
    //Task<DonorDto> CreateDonorAsync(CreateDonorDto donor);
    //Task<IEnumerable<DonorDto>> getByNameAsync(string name);
    //Task<DonorDto> GetByEmailAsync(string email);
    //Task<DonorDto> GetByPresentIdAsync(int presentId);
    //Task<DonorDto?> GetByIdAsync(int id);

    [ApiController]
    [Route("api/[controller]")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;
        private readonly ILogger<DonorController> _logger;



        public DonorController(
             IDonorService donorService,
            ILogger<DonorController> logger)
        {
            _donorService = donorService;
            _logger = logger;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonorDto>>> GetAll()
        {
            var donors = await _donorService.GetAllAsync();
            return Ok(donors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonorDto>> getById(int id)
        {
            var donor = await _donorService.GetByIdAsync(id);
            if (donor == null)
                return NotFound(new { massage = $"donor with UserId: {id} not found" });

            return Ok(donor);
        }

        [HttpGet("present/{presentId}")]
        public async Task<ActionResult<DonorDto>> GetByPresent(int presentId)
        {
            var donor = await _donorService.GetByPresentIdAsync(presentId);
            return Ok(donor);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<DonorDto>> GetByEmail(string email)
        {
            var donor = await _donorService.GetByEmailAsync(email);
            return Ok(donor);
        }
        [HttpGet("name/{name}")]
        public async Task<ActionResult<DonorDto>> GetByName(string name)
        {
            var donor = await _donorService.getByNameAsync(name);
            return Ok(donor);
        }

        [HttpPost]
        public async Task<ActionResult<DonorDto>> Create([FromBody] CreateDonorDto donorDto)
        {
            try
            {
                var donor = await _donorService.CreateDonorAsync(donorDto);
                _logger.LogInformation("Donor created with ID: {DonorId}", donor.Id);
                return CreatedAtAction(nameof(getById), new { id = donor.Id }, donor);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("error while creating donor", donorDto.Name);

                return BadRequest(new { massage = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<DonorDto>> Update(int id, [FromBody] UpdateDonorDto donorDto)
        {

            try
            {
                var donor = await _donorService.UpdateAsync(donorDto);
                if (donor == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });
                }

                return Ok(donor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DonorDto>> Delete(int id)
        {
            bool result = false;

            try {
                result = await _donorService.DeleteAsync(id);

            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
          
            if (!result)
            {
                return NotFound(new { message = $"Donor with ID {id} not found." });
            }

            return NoContent();
        }
        

    }
}
