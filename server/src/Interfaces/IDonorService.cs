using a.Dto;
using a.Models;
using NET.Models;

namespace a.Interfaces
{
    public interface IDonorService
    {
        Task<IEnumerable<DonorDto>> GetAllAsync();
        Task<DonorDto> UpdateAsync(UpdateDonorDto donor);
        Task<bool> DeleteAsync(int id);
        Task<DonorDto> CreateDonorAsync(CreateDonorDto donor);
        Task<IEnumerable<DonorDto>> getByNameAsync(string name);
        Task<DonorDto> GetByEmailAsync(string email);
        Task<DonorDto> GetByPresentIdAsync(int presentId);
        Task<DonorDto?> GetByIdAsync(int id);
    }
}
