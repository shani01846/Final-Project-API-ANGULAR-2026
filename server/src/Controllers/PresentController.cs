using a.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApi.DTOs;
using StoreApi.Interfaces;
namespace a.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PresentController : ControllerBase
    {
        private readonly IPresentService _presentService;
        private readonly ILogger<PresentController> _logger;

        public PresentController(
            IPresentService presentService,
            ILogger<PresentController> logger)
        {
            _presentService = presentService;
            _logger = logger;
        }
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<PresentDto>>> GetAllPaged([FromQuery] PaginationParams paginationParams)
        {
            var presents = await _presentService.GetAllPresentsPagedAsync(paginationParams);
            return Ok(presents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PresentDto>> getById(int id)
        {
            var present = await _presentService.GetPresentByIdAsync(id);
            if (present == null)
                return NotFound(new { massage = $"present with UserId: {id} not found" });

            return Ok(present);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<PresentDto>>> GetByCategory(int categoryId)
        {
            var presents = await _presentService.GetPresentsByCategoryAsync(categoryId);
            return Ok(presents);
        }
        //create present dto
        //[Authorize(Roles = "Donor")]
        [HttpPost]
        public async Task<ActionResult<PresentDto>> Create([FromBody] CreatePresentDto presentDto)
        {
            try
            {
                var present = await _presentService.CreatePresentAsync(presentDto);
                return CreatedAtAction(nameof(getById), new { id = present.Id }, present);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { massage = ex.Message });
            }
        }
        //[Authorize(Roles = "Admin,Donor")]

        [HttpPut("{id}")]
        public async Task<ActionResult<PresentDto>> Update(int id, [FromBody] UpdatePresentDto presentDto)
        {

            try
            {
                var present = await _presentService.UpdatePresentAsync(id, presentDto);
                if (present == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });
                }

                return Ok(present);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

      //  [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PresentDto>> Delete(int id)
        {

            var result = await _presentService.DeletePresentAsync(id);

            if (!result)
            {
                return NotFound(new { message = $"Present with ID {id} not found." });
            }

            return NoContent();
        }

    }
}