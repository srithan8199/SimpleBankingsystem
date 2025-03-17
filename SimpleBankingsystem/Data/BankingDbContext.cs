using Microsoft.EntityFrameworkCore;
using SimpleBankingSystem.Models;

namespace SimpleBankingSystem.Data
{
    public class BankingDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=srithan;User Id=sa;Password=Mihira@2021;Database=task;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>().Property(a => a.AccountId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnOrder(0);
            modelBuilder.Entity<Account>().Property(a => a.Balance).HasDefaultValue(0.00);
            modelBuilder.Entity<Account>().HasIndex(a => a.AccountNumber).IsUnique();
            modelBuilder.Entity<Account>().Property(a => a.AccountNumber).IsRequired();
        }
    }
}
