﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yarsey.EntityFramework;

namespace Yarsey.EntityFramework.Migrations
{
    [DbContext(typeof(YarseyDbContext))]
    [Migration("20210730142327_AddAccounts")]
    partial class AddAccounts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Yarsey.Domain.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Account");
                });

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

            modelBuilder.Entity("Yarsey.Domain.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adress")
                        .HasColumnType("TEXT");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Customer_Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Due")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ref_no")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Customer_Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AssociatedAccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<double>("ProductCost")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductUOM")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssociatedAccountId");

                    b.HasIndex("BusinessId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.ProductSelection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<double>("Discount")
                        .HasColumnType("REAL");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PricePerItem")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SelectedProductId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Tax")
                        .HasColumnType("REAL");

                    b.Property<string>("Word")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("SelectedProductId");

                    b.ToTable("ProductSelection");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.RunningNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModuleName")
                        .HasColumnType("TEXT");

                    b.Property<int>("RunningNo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RunningNumbers");
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

                    b.Property<int?>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Customer_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<int>("SaleCategory")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TransactionId");

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

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Account", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Accounts")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Customer", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Customers")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Invoice", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Invoices")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Yarsey.Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("Customer_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Product", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Account", "AssociatedAccount")
                        .WithMany()
                        .HasForeignKey("AssociatedAccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Products")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AssociatedAccount");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.ProductSelection", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Invoice", null)
                        .WithMany("ProductsSelected")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Yarsey.Domain.Models.Product", "SelectedProduct")
                        .WithMany()
                        .HasForeignKey("SelectedProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SelectedProduct");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Sale", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Sales")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Yarsey.Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Yarsey.Domain.Models.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId");

                    b.Navigation("Customer");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Transaction", b =>
                {
                    b.HasOne("Yarsey.Domain.Models.Business", null)
                        .WithMany("Transactions")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Business", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Customers");

                    b.Navigation("Invoices");

                    b.Navigation("Products");

                    b.Navigation("Sales");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Yarsey.Domain.Models.Invoice", b =>
                {
                    b.Navigation("ProductsSelected");
                });
#pragma warning restore 612, 618
        }
    }
}
