using a.Dto;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Interfaces;

namespace a.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class CategoryController : ControllerBase
        {
            private readonly ICategoryService _categoryService;
            private readonly ILogger<CategoryController> _logger;

            public CategoryController(
                ICategoryService categoryService,
                ILogger<CategoryController> logger)
            {
                _categoryService = categoryService;
                _logger = logger;
            }

            [HttpGet]
            //[ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
            public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }

            [HttpGet("{id}")]
            //[ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
            //[ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<CategoryDto>> GetById(int id)
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }

                return Ok(category);
            }

            [HttpPost]
            //[ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
            //[ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryDto createDto)
            {
                try
                {
                    var category = await _categoryService.CreateCategoryAsync(createDto);
                    return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpPut("{id}")]
            [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] CategoryDto updateDto)
            {
                try
                {
                    var category = await _categoryService.UpdateCategoryAsync(id, updateDto);

                    if (category == null)
                    {
                        return NotFound(new { message = $"Category with ID {id} not found." });
                    }

                    return Ok(category);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {
                var result = await _categoryService.DeleteCategoryAsync(id);

                if (!result)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }

                return NoContent();
            }
        }
    }

