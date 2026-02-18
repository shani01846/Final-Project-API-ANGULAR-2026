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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Present>(entity =>
           { entity.HasKey(x => x.Id);
               entity.Property(x => x.Name).IsRequired();
               entity.HasOne(x=>x.Donor)
               .WithMany(x=>x.Presents)
               .HasForeignKey(x=>x.DonorId)
               .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<LotteryResult>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(e => e.Winner)
                .WithMany()
                .HasForeignKey(e => e.WinnerUserId)
                .OnDelete(DeleteBehavior.NoAction);


                entity.HasOne(e => e.Present)
                .WithMany()  // חד-כיווני
                .HasForeignKey(e => e.PresentId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(e => e.Purchases)
                .WithOne(e=>e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(e => e.User)
                .WithMany(e => e.Purchases)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasKey(x => x.Id);
                entity.HasOne(e => e.Present)
                .WithMany(e => e.Purchases)
                .HasForeignKey(e => e.PresentId)
                .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(e => e.Presents)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
             });
        }
    }
}
