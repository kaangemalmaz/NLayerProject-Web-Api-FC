using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.DataAccess.Configurations;
using UdemyNLayerProject.DataAccess.Seeds;
using UdemyNLayerProject.Entity.Models;

namespace UdemyNLayerProject.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //veritabanında product ve categorilerin sqle çevrilirken nasıl çevrileceğini belirtiriz.
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] { 1, 2 }));
            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1, 2 }));


            modelBuilder.Entity<Person>().HasKey(i => i.Id);
            modelBuilder.Entity<Person>().Property(i => i.Id).UseIdentityColumn();
            modelBuilder.Entity<Person>().Property(i => i.Name).HasMaxLength(100);
            modelBuilder.Entity<Person>().Property(i => i.Surname).HasMaxLength(100);
        }
    }
}
