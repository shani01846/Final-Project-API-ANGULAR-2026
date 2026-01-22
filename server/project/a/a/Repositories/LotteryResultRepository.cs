using a.Data;
using a.Interfaces;
using a.Models;
using Microsoft.EntityFrameworkCore;

namespace a.Repositories
{
    public class LotteryResultRepository : ILotteryResultRepository
    {
        private static readonly Random R = new Random();

        private readonly ApplicationDbContext _context;

        public LotteryResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LotteryResult> createWinner(LotteryResult lr)
        {
            _context.LotteryResults.Add(lr);
            await _context.SaveChangesAsync();
            return lr;
        }

        public async Task<IEnumerable<LotteryResult>> GetAll()
        {
            return await _context.LotteryResults
                .Include(l=>l.Winner)
                .Include(p=>p.Present)
                .ToListAsync();
        }

        public async Task<LotteryResult> makeLottery(int presentId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            var p = await _context.Presents
                .Include(p=>p.Purchases)
                .ThenInclude(p=>p.User)
                .FirstOrDefaultAsync(p=>p.Id == presentId);

            int count= p.Purchases.Count;
            if (count == 0)  return null;

            int randomInd = R.Next(count);

            var winner = p.Purchases
                .Skip(randomInd)
                .FirstOrDefault()!.User;

            //יצירת זוכה
           var result = new LotteryResult() { PresentId = presentId, WinnerUserId = winner.Id};

           await createWinner(result);

            //עדכון שנעשתה ההגרלה למתנה זו
           p.IsLotteryDone=true;
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return result;

        }
    }
}
