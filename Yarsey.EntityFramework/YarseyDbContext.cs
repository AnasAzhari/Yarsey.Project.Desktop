using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yarsey.Domain;
using Yarsey.Domain.Models;


namespace Yarsey.EntityFramework
{
    public class YarseyDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Business> Businesses { get; set; }
        public YarseyDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Email);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Adress);
                entity.Property(e => e.PhoneNo);
                entity.Property(e => e.CompanyName);
                entity.Property(e => e.Note);


            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.Property(e => e.Email);
                entity.Property(e => e.BusinessName).IsRequired();
                entity.Property(e => e.Adresss);
                entity.Property(e => e.PhoneNo);
                entity.Property(e => e.RegistrationNo);
                entity.Property(e => e.Image);
                entity.HasMany(e => e.Customers).WithOne().IsRequired().OnDelete(DeleteBehavior.ClientCascade);

            });


            base.OnModelCreating(modelBuilder);

        }
    }
}
