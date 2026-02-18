
using NET.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StoreApi.Interfaces;

public interface IPresentRepository
{
    Task<IEnumerable<Present>> GetAllAsync();
    Task<(IEnumerable<Present> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<Present?> GetByIdAsync(int id);
    Task<Present> CreateAsync(Present product);
    Task<Present?> UpdateAsync(Present product);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Present>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Present>> SearchByNameAsync(string searchTerm);
    Task<IEnumerable<Present>> SearchByDonorNameAsync(string searchTerm);

    Task<IEnumerable<Present>> SearchByNumOfPurchasesAsync(int num);
}
