using a.Data;
using Microsoft.EntityFrameworkCore;
using NET.Models;
using StoreApi.Interfaces;

namespace a.Repositories
{
    public class PresentRepository : IPresentRepository
    {
        private readonly ApplicationDbContext _context;

        public PresentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Present> CreateAsync(Present product)
        {
            _context.Presents.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var present = await _context.Presents.FindAsync(id);
            if (present == null) return false;

            _context.Presents.Remove(present);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Presents.AnyAsync(p=>p.Id == id);
        }

        public async Task<IEnumerable<Present>> GetAllAsync()
        {
            return await _context.Presents
                            .Include(p => p.Category)
                            .Include(p => p.Donor)
                            .ToListAsync();
        }
        public async Task<(IEnumerable<Present> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Presents.CountAsync();

            var items = await _context.Presents
                .Include(p => p.Category)
                .Include(p => p.Donor)
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items,totalCount);
        }

        public async Task<IEnumerable<Present>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Presents
                .Where(p => p.CategoryId == categoryId)
                 .ToListAsync();
        }

        public async Task<Present?> GetByIdAsync(int id)
        {
            return await _context.Presents
                .Include(d=>d.Donor)
                .FirstOrDefaultAsync(p=>p.Id== id);
        }

        public async Task<IEnumerable<Present>> SearchByDonorNameAsync(string name)
        {
            return await _context.Presents
               .Where(p => p.Donor.Name == name)
               .ToListAsync();
        }

        public async Task<IEnumerable<Present>> SearchByNameAsync(string name)
        {
            return await _context.Presents
                          .Where(p => p.Name == name)
                          .ToListAsync();
        }


        public async Task<IEnumerable<Present>> SearchByNumOfPurchasesAsync(int num)
        {

            return await _context.Presents
                         .Where(p => p.Purchases.Count >= num)
                         .ToListAsync();
        }

        public async Task<Present?> UpdateAsync(Present present)
        {
            var existingPresent = await _context.Presents.FindAsync(present.Id);
            if (existingPresent == null) return null;
            _context.Entry(existingPresent).CurrentValues.SetValues(present);
            await _context.SaveChangesAsync();
            return existingPresent;
        }
    }
}
