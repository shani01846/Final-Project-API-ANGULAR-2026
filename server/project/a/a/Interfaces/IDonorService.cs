using a.Dto;
using a.Models;
using NET.Models;

namespace a.Interfaces
{
    public interface IDonorService
    {
        Task<IEnumerable<DonorDto>> GetAllAsync();
        Task<DonorDto> Update(UpdateDonorDto donor);
        Task<bool> Delete(int id);
        Task<DonorDto> CreateDonor(CreateDonorDto donor);
        Task<IEnumerable<DonorDto>> getByName(string name);
        Task<DonorDto> GetByEmail(string email);
        Task<DonorDto> GetByPresentId(int presentId);
        Task<DonorDto?> GetByIdAsync(int id);
    }
}
