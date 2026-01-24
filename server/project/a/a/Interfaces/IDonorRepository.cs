using a.Dto;
using a.Models;

namespace a.Interfaces
{
    public interface IDonorRepository
    {
        Task<IEnumerable<Donor>> GetAllAsync();
        Task<Donor> UpdateAsync(Donor donor);
        Task<bool> DeleteAsync(int id);
        Task<Donor> CreateDonorAsync(Donor donor);
        Task<IEnumerable<Donor?>> GetByNameAsync(string name);
        Task<Donor?> GetByEmailAsync(string email);
        Task<Donor?> GetByPresentIdAsync(int presentId);
        Task<bool> ExistsAsync(int id);
        Task<Donor?> GetByIdAsync(int id);

    }
}
