﻿// <auto-generated />
using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace KEC.ECommerce.Data.Migrations
{
    [DbContext(typeof(ECommerceDataContext))]
    [Migration("20180603202300_RenameCartToShoppingCart")]
    partial class RenameCartToShoppingCart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.LineItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int>("PublicationId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PublicationId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("CustomerGuid");

                    b.Property<string>("OrderNumber");

                    b.Property<DateTime>("SubmittedAt");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int>("PaymentMethod");

                    b.Property<string>("TransactedBy");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<string>("TransactionNumber");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("LevelId");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("PublisherId");

                    b.Property<int>("Quantity");

                    b.Property<int>("SubjectId");

                    b.Property<string>("ThumbnailUrl");

                    b.Property<string>("Title");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LevelId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Guid");

                    b.Property<DateTime>("ModifiedAt");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartId");

                    b.Property<int>("PublicationId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("PublicationId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.LineItem", b =>
                {
                    b.HasOne("KEC.ECommerce.Data.Models.Order", "Order")
                        .WithMany("LineItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KEC.ECommerce.Data.Models.Publication", "Publication")
                        .WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Payment", b =>
                {
                    b.HasOne("KEC.ECommerce.Data.Models.Order", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("KEC.ECommerce.Data.Models.Payment", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.Publication", b =>
                {
                    b.HasOne("KEC.ECommerce.Data.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KEC.ECommerce.Data.Models.Category", "Category")
                        .WithMany("Publications")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KEC.ECommerce.Data.Models.Level")
                        .WithMany("Publications")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KEC.ECommerce.Data.Models.Publisher", "Publisher")
                        .WithMany("Publications")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KEC.ECommerce.Data.Models.Subject", "Subject")
                        .WithMany("Publications")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KEC.ECommerce.Data.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("KEC.ECommerce.Data.Models.ShoppingCart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KEC.ECommerce.Data.Models.Publication", "Publication")
                        .WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
