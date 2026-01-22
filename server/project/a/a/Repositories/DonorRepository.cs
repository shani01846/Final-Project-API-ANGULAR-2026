using a.Data;
using a.Interfaces;
using a.Models;
using Microsoft.EntityFrameworkCore;

namespace a.Repositories
{
    public class DonorRepository : IDonorRepository
    { 

        private readonly ApplicationDbContext _context;

        public DonorRepository (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Donor> CreateDonor(Donor donor)
        {
             _context.Donors.Add(donor);
            await _context.SaveChangesAsync();
            return donor;
        }

        public async Task<bool> Delete(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if(donor == null) return false;
            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();
            return true;
        
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Donors.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Donor>> GetAllAsync()
        {

            return await _context.Donors
                .Include(p => p.Presents)
                .ToListAsync();
        }

      

        public async Task<Donor?> GetByEmail(string email)
        {
            return await _context.Donors
            .FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public async Task<Donor?> GetByIdAsync(int id)
        {
            return await _context.Donors
               .Include(d => d.Presents)
               .FirstOrDefaultAsync(p => p.Id == id) ?? null;
        }

        public async Task<IEnumerable<Donor?>> getByName(string name)
        {
            return await _context.Donors
             .Where(u => u.Name == name)
            .Include (p => p.Presents)
            .ToListAsync();
        }

        public async Task<Donor?> GetByPresentId(int presentId)
        {
            return await _context.Presents
                .Where(p=>p.Id == presentId)
                .Select(p=>p.Donor)
                .FirstOrDefaultAsync();
                
        }

        public async Task<Donor> Update(Donor donor)
        {
            var existing = await _context.Donors.FindAsync(donor.Id);
            if (existing == null) return null;
           _context.Entry(existing).CurrentValues.SetValues(donor);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
