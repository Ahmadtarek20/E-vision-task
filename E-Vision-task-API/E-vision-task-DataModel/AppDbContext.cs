using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Vision_task.Domain;
using Microsoft.EntityFrameworkCore;

namespace E_Vision_task.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(50);

        }
        public DbSet<Product> Products { get; set; }

    }
}