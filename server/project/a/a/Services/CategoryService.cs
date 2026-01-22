using a.Dto;
using NET.Models;
using StoreApi.Interfaces;

namespace a.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto createDto)
        {
            var category = new Category
            {
                Name = createDto.Name
            };
                  var createdCategory = await _.CreateAsync(category);
            _logger.LogInformation("Category created with ID: {CategoryId}", createdCategory.Id);

            return MapToResponseDto(createdCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToResponseDto);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category!=null ? MapToResponseDto(category) : null;
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryDto updateDto)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null) return null;

            if (updateDto.Name != null) existingCategory.Name = updateDto.Name;

            var updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);
            return updatedCategory != null ? MapToResponseDto(updatedCategory) : null;
        }
        private static CategoryDto MapToResponseDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

    }
}
