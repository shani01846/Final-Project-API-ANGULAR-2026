

using a.Dto;

namespace StoreApi.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CategoryDto createDto);
    Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryDto updateDto);
    Task<bool> DeleteCategoryAsync(int id);
}
