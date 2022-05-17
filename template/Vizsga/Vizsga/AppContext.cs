using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizsga.Models;

namespace Vizsga
{
    public class AppContext : DbContext
    {
        public DbSet<OrderModel> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=példa;uid=root;pwd=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel() { Id = 1, Name = "asd" },
                new CategoryModel() { Id = 2, Name = "asd" },
                new CategoryModel() { Id = 3, Name = "asd" },
                new CategoryModel() { Id = 4, Name = "asd" },
                new CategoryModel() { Id = 5, Name = "asd" }
            );
        }

    }
}
