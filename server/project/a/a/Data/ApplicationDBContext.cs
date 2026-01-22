using a.Models;
using Microsoft.EntityFrameworkCore;
using NET.Models;

namespace a.Data
{
    public class ApplicationDbContext: DbContext 
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Present> Presents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<LotteryResult> LotteryResults { get; set; }
        public DbSet<Donor> Donors { get; set; }

    }
}
