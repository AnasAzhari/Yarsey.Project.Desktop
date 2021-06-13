﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yarsey.EntityFramework;

namespace Yarsey.EntityFramework.Migrations
{
    [DbContext(typeof(YarseyDbContext))]
    partial class YarseyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Yarsey.Domain.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adresss")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Image")
                        .HasColumnType("BLOB");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("RegistrationNo")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adress")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNo")
                        .HasMaxLength(14)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Customer_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<int>("SaleCategory")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Customer_id")
                        .IsUnique();

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sales_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Sales_id")
                        .IsUnique();

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Customer", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Customers")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Sale", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Sales")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Yarsey.Domain.Models.Customer", "Customer")
                        .WithOne()
                        .HasForeignKey("Yarsey.Domain.Models.Sale", "Customer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Transaction", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Transactions")
                        .HasForeignKey("BusinessId");

                    b.HasOne("Yarsey.Domain.Models.Sale", null)
                        .WithOne("Transaction")
                        .HasForeignKey("Yarsey.Domain.Models.Transaction", "Sales_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Business", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Sales");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Sale", b =>
                {
                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
