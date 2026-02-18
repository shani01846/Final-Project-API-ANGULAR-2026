using a.Dto;
using NET.Models;
using StoreApi.DTOs;

namespace StoreApi.Interfaces;

public interface IPresentService
{
    Task<IEnumerable<PresentDto>> GetAllPresentsAsync();
    Task<PagedResult<PresentDto>> GetAllPresentsPagedAsync(PaginationParams paginationParams);
    Task<PresentDto?> GetPresentByIdAsync(int id);
    Task<IEnumerable<PresentDto>> GetPresentsByCategoryAsync(int categoryId);
    Task<IEnumerable<PresentDto>> SearchPresentsByNameAsync(string searchTerm);
    Task<IEnumerable<PresentDto>> SearchByDonorNameAsync(string searchTerm);
    Task<IEnumerable<PresentDto>> SearchByNumOfPurchasesAsync(string searchTerm);
    Task<PresentDto> CreatePresentAsync(CreatePresentDto createDto);
    Task<PresentDto?> UpdatePresentAsync(int id, UpdatePresentDto updateDto);
    Task<bool> DeletePresentAsync(int id);


}
