﻿using System;
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
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RunningNumber> RunningNumbers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<ProductSalesDetail> ProductSalesDetails { get; set; }
        public DbSet<ProductPurchaseDetail> ProductPurchaseDetails { get; set; }
        public YarseyDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Business>(entity =>
            {
                entity.Property(e => e.Email);
                entity.Property(e => e.BusinessName).IsRequired();
                entity.Property(e => e.Adresss);
                entity.Property(e => e.PhoneNo);
                entity.Property(e => e.RegistrationNo);
                entity.Property(e => e.Image);
                entity.HasMany(e => e.Customers).WithOne().IsRequired().OnDelete(DeleteBehavior.ClientCascade);
                entity.HasMany(e => e.Sales).WithOne().IsRequired().OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(e => e.Products).WithOne().IsRequired().OnDelete(DeleteBehavior.ClientCascade);
                entity.HasMany(e => e.Invoices).WithOne().IsRequired().OnDelete(DeleteBehavior.ClientCascade);
                entity.HasMany(e => e.Accounts).WithOne().IsRequired().OnDelete(DeleteBehavior.ClientCascade);
                entity.HasMany(e => e.RunningNumbers).WithOne().IsRequired().OnDelete(DeleteBehavior.ClientCascade);


            });


            modelBuilder.Entity<RunningNumber>(entity =>
            {
                entity.Property(e => e.ModuleName);
                entity.Property(e => e.RunningNo);
                entity.Property(e => e.Prefix);
            });

   

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Email);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Adress);
                entity.Property(e => e.PhoneNo);
                entity.Property(e => e.CompanyName);
                entity.Property(e => e.Note);

            });

            //modelBuilder.Entity<Sale>(entity =>
            //{
            //    entity.Property(e => e.CreatedTime);
            //    entity.Property(e => e.SaleCategory).HasConversion<int>();
            //    entity.HasOne(e => e.Customer).WithOne().OnDelete(DeleteBehavior.Cascade).HasForeignKey<Sale>(s=>s.Customer_id);
            //    entity.Property(e => e.Notes);
            //    entity.HasOne(e => e.Transaction).WithOne().OnDelete(DeleteBehavior.Cascade).HasForeignKey<Transaction>(b=>b.Sales_id);


            //});
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedTime);
                entity.Property(e => e.ProductName);
                entity.Property(e => e.Notes);
                entity.Property(e => e.ProductUOM).HasConversion(v => v.ToString(), v => (ProductUom)Enum.Parse(typeof(ProductUom), v));
                //entity.Property(e => e.ProductUOM).HasConversion<string>();
                entity.HasOne(e => e.ProductSalesDetail).WithOne(e => e.Product).HasForeignKey<ProductSalesDetail>(e => e.ProductId).OnDelete(DeleteBehavior.ClientCascade);
                entity.HasOne(e => e.ProductPurchaseDetail).WithOne(e => e.Product).HasForeignKey<ProductPurchaseDetail>(e => e.ProductId).OnDelete(DeleteBehavior.ClientCascade);

            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceDate);
                entity.Property(e => e.Due);
                entity.Property(e => e.Adress);
                entity.Property(e => e.InvoiceStatus).HasConversion(v => v.ToString(), v => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), v)); 
                entity.HasOne(e => e.Customer).WithMany().HasForeignKey(e => e.Customer_Id).OnDelete(DeleteBehavior.NoAction);
               
                entity.HasMany(e => e.ProductsSelected).WithOne().IsRequired().OnDelete(DeleteBehavior.ClientCascade);
               
            });

            modelBuilder.Entity<ProductSelection>(entity =>
            {
                entity.Property(e => e.Discount).HasConversion<double>();
                entity.Property(e => e.Tax).HasConversion<double>();
                entity.HasOne(e => e.SelectedProduct).WithMany().HasForeignKey(e=>e.SelectedProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                entity.Property(e => e.Quantity);                
            });

            //modelBuilder.Entity<ProductSalesDetail>(entity =>
            //{
             
            //    entity.Property(e => e.SalesPrice);
            //    entity.Property(e => e.SalesDescription);
            //    //entity.Property(e => e.SalesTax);
            //});
            //modelBuilder.Entity<ProductPurchaseDetail>(entity =>
            //{
                
            //    entity.Property(e => e.PurchasePrice);
            //    entity.Property(e => e.PurchaseDescription);
            //    //entity.Property(e => e.PurchaseTax);
            //});


           

        }
    }
}
