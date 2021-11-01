using Microsoft.EntityFrameworkCore;
using StockData.Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.Contexts
{
    public class StockDataDbContext : DbContext, IStockDataDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public StockDataDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<StockPrice>()
                .HasKey(Sp => Sp.Id);

            modelBuilder.Entity<StockPrice>()
              .HasOne(Sp => Sp.Company)
              .WithMany(c => c.StockPrices)
              .HasForeignKey(Sp => Sp.CompanyId);

            


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }

    }
}
