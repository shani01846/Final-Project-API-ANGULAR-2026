using a.Dto;
using a.Models;

namespace a.Interfaces
{
    public interface IDonorRepository
    {
        Task<IEnumerable<Donor>> GetAllAsync();
        Task<Donor> Update(Donor donor);
        Task<bool> Delete(int id);
        Task<Donor> CreateDonor(Donor donor);
        Task<IEnumerable<Donor?>> getByName(string name);
        Task<Donor?> GetByEmail(string email);
        Task<Donor?> GetByPresentId(int presentId);
        Task<bool> ExistsAsync(int id);
        Task<Donor?> GetByIdAsync(int id);

    }
}
