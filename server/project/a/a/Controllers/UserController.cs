using a.Controllers;
using a.Dto;
using Microsoft.AspNetCore.Mvc;
using NET.Dto;
using NET.Models;
using StoreApi.Interfaces;

namespace NET.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<PresentController> _logger;

        public UserController(
            IUserService userService,
            ILogger<PresentController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<RgisterDto>> Create([FromBody] RgisterDto createDto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RgisterDto>> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RgisterDto>> Update(int id, [FromBody] RgisterDto updateDto)
        {
            try
            {
                var user = await _userService.UpdateUserAsync(id, updateDto);

                if (user == null)
                {
                    return NotFound(new { message = $"User with ID {id} not found." });
                }

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return NoContent();
        }

    }
}
