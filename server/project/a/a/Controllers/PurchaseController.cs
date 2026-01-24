using a.Dto;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Interfaces;

namespace a.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController: ControllerBase
    {
        private readonly IPurchaseService _orderService;
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(
            IPurchaseService orderService,
            ILogger<PurchaseController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<OrderResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetAll()
        {
            var orders = await _orderService.GetAllPurchasesAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        //[ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseDto>> GetById(int id)
        {
            var order = await _orderService.GetPurchaseByIdAsync(id);

            if (order == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        //[ProducesResponseType(typeof(IEnumerable<PurchaseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetByUserId(int userId)
        {
            var orders = await _orderService.GetPurchasesByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        //[ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PurchaseDto>> Create([FromBody] PurchaseDto createDto)
        {
            try
            {
                var order = await _orderService.CreatePurchaseAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpPut("{id}")]
        //[ProducesResponseType(typeof(PurchaseDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<PurchaseDto>> UpdateAsync(int id, [FromBody] PurchaseDto updateDto)
        //{
        //    try
        //    {
        //        var order = await _orderService.UpdatePurchaseAsync(id, updateDto);

        //        if (order == null)
        //        {
        //            return NotFound(new { message = $"Order with ID {id} not found." });
        //        }

        //        return Ok(order);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.DeletePurchaseAsync(id);

            if (!result)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            return NoContent();
        }
    }
}
