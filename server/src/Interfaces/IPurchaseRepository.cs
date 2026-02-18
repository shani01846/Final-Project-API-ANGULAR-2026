
using NET.Models;

namespace StoreApi.Interfaces;

public interface IPurchaseRepository
{
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase?> GetByIdAsync(int id);
    Task<IEnumerable<Purchase>> GetByUserIdAsync(int userId);
    Task<Purchase> CreateAsync(Purchase order);
    Task<Purchase?> UpdateAsync(Purchase order);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Purchase>> GetByPresentIdAsync(int id);

    Task<decimal> GetSumForAllAsync();
    Task<IEnumerable<Purchase>> getAllPurchasesIsDraftAsync(int id);




}