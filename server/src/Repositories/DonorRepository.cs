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
        public async Task<Donor> CreateDonorAsync(Donor donor)
        {
             _context.Donors.Add(donor);
            await _context.SaveChangesAsync();
            return donor;
        }

       
        public async Task<bool> DeleteAsync(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if(donor == null) return false;
            var hasPresents = await _context.Presents.AnyAsync(p => p.DonorId == id);
            if (hasPresents)
                throw new InvalidOperationException("לא ניתן למחוק את התורם כיוון שקיימות מתנות המשויכות אליו.");

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

      

        public async Task<Donor?> GetByEmailAsync(string email)
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

        public async Task<IEnumerable<Donor?>> GetByNameAsync(string name)
        {
            return await _context.Donors
             .Where(u => u.Name == name )
            .Include (p => p.Presents)
            .ToListAsync();
        }

        public async Task<Donor?> GetByPresentIdAsync(int presentId)
        {
            return await _context.Presents
                .Where(p=>p.Id == presentId)
                .Select(p=>p.Donor)
                .FirstOrDefaultAsync();
                
        }

        public async Task<Donor> UpdateAsync(Donor donor)
        {
            var existing = await _context.Donors.FindAsync(donor.Id);
            if (existing == null) return null;
           _context.Entry(existing).CurrentValues.SetValues(donor);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
