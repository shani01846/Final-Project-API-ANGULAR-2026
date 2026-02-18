
using a.Dto;
using NET.Models;
using StoreApi.DTOs;

namespace StoreApi.Interfaces;

public interface IPurchaseService
{
    Task<IEnumerable<PurchaseDto>> GetAllPurchasesAsync();
    Task<PurchaseDto?> GetPurchaseByIdAsync(int id);
    Task<IEnumerable<PurchaseDto>> GetPurchasesByUserIdAsync(int userId);
    Task<PurchaseDto> CreatePurchaseAsync(PurchaseDto createDto);
    Task<PurchaseDto?> UpdatePurchaseAsync(int id, UpdatePurchaseDto updateDto);
    Task<bool> DeletePurchaseAsync(int id);

    Task<IEnumerable<PurchaseDto>> GetByPresentIdAsync(int id);
    Task<decimal> GetSumForAllAsync();
    Task<IEnumerable<PurchaseDto>> GetAllPurchasesIsDraftAsync(int id);
}
   