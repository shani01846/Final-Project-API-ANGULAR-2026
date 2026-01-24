using a.Data;
using Microsoft.EntityFrameworkCore;
using NET.Models;
using StoreApi.Interfaces;

namespace a.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {

        private readonly ApplicationDbContext _context;

        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Purchase> CreateAsync(Purchase order)
        {

            _context.Purchases.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null) return false;
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Purchases.AnyAsync(o=>o.Id==id);

        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _context.Purchases
                .Include(u=>u.User)
                .Include(p=>p.Present)
                .ToListAsync();

        }

        public async Task<IEnumerable<Purchase>> getAllPurchasesIsDraftAsync()
        {

            return await _context.Purchases
                .Where(p => p.IsDraft == true)
                .Include(u => u.User)
                .Include(p => p.Present)
                .ToListAsync();
        }

        public async Task<Purchase?> GetByIdAsync(int id)
        {

            return await _context.Purchases
                .Include(u=>u.User)
                .Include(p=>p.Present)
                .FirstOrDefaultAsync(o=>o.Id==id);

        //    var purchase = await _context.Purchases.FindAsync(id);
        //    if (purchase == null) return null;
        //    return purchase;
        }

        public async Task<IEnumerable<Purchase>> GetByPresentIdAsync(int id)
        {

            return await _context.Purchases
               .Where(u => u.PresentId == id)
               .Include(p => p.Present)
               .Include(u => u.User)
               .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetByUserIdAsync(int userId)
        {
                 return await _context.Purchases
                .Where(u => u.UserId == userId)
                .Include(p => p.Present)
                .ToListAsync();
        }

        public async Task<decimal> GetSumForAllAsync()
        {
            var list = await _context.Purchases
                .Include(p => p.Present)
                .ToListAsync();
           var sum= list.Sum(u => u.Present.Price*u.NumOfTickets);

            return sum;

        }

        public async Task<Purchase?> UpdateAsync(Purchase purchase)
        {
            var existingPurchase = await _context.Purchases.FindAsync(purchase.Id);
            if(existingPurchase == null)  return null;
            _context.Entry(existingPurchase).CurrentValues.SetValues(purchase);

            await _context.SaveChangesAsync();
            return existingPurchase;
        }
    }
}
