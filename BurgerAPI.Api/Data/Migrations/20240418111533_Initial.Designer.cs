﻿// <auto-generated />
using System;
using BurgerAPI.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BurgerAPI.Api.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240418111533_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.BurgerOption", b =>
                {
                    b.Property<int>("BurgerId")
                        .HasColumnType("int");

                    b.Property<string>("Meat")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Letuce")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Bacon")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CacaramelizedOnion")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("FriedEgg")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("RegOnion")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Tomato")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CheeseType")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Sauce")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BurgerId", "Meat", "Letuce", "Bacon", "CacaramelizedOnion", "FriedEgg", "RegOnion", "Tomato", "CheeseType", "Sauce");

                    b.ToTable("BurgerOptions");

                    b.HasData(
                        new
                        {
                            BurgerId = 1,
                            Meat = "beef",
                            Letuce = "no",
                            Bacon = "yes",
                            CacaramelizedOnion = "no",
                            FriedEgg = "no",
                            RegOnion = "no",
                            Tomato = "no",
                            CheeseType = "none",
                            Sauce = "guacamole"
                        },
                        new
                        {
                            BurgerId = 2,
                            Meat = "beef",
                            Letuce = "yes",
                            Bacon = "no",
                            CacaramelizedOnion = "no",
                            FriedEgg = "yes",
                            RegOnion = "yes",
                            Tomato = "yes",
                            CheeseType = "cheddar",
                            Sauce = "none"
                        },
                        new
                        {
                            BurgerId = 3,
                            Meat = "beef",
                            Letuce = "no",
                            Bacon = "no",
                            CacaramelizedOnion = "yes",
                            FriedEgg = "no",
                            RegOnion = "no",
                            Tomato = "no",
                            CheeseType = "goat cheese",
                            Sauce = "none"
                        },
                        new
                        {
                            BurgerId = 4,
                            Meat = "Double Beef",
                            Letuce = "no",
                            Bacon = "no",
                            CacaramelizedOnion = "no",
                            FriedEgg = "no",
                            RegOnion = "no",
                            Tomato = "no",
                            CheeseType = "cheddar,camembert,roquefort,mozzarella",
                            Sauce = "cheddar sauce"
                        },
                        new
                        {
                            BurgerId = 5,
                            Meat = "beef",
                            Letuce = "no",
                            Bacon = "no",
                            CacaramelizedOnion = "no",
                            FriedEgg = "no",
                            RegOnion = "no",
                            Tomato = "no",
                            CheeseType = "cheddar",
                            Sauce = "none"
                        },
                        new
                        {
                            BurgerId = 6,
                            Meat = "chicken",
                            Letuce = "yes",
                            Bacon = "no",
                            CacaramelizedOnion = "no",
                            FriedEgg = "no",
                            RegOnion = "no",
                            Tomato = "yes",
                            CheeseType = "cheddar",
                            Sauce = "mayonnaise"
                        },
                        new
                        {
                            BurgerId = 7,
                            Meat = "beef",
                            Letuce = "no",
                            Bacon = "yes",
                            CacaramelizedOnion = "no",
                            FriedEgg = "yes",
                            RegOnion = "no",
                            Tomato = "no",
                            CheeseType = "double cheddar",
                            Sauce = "mayonnaise"
                        },
                        new
                        {
                            BurgerId = 8,
                            Meat = "double beef",
                            Letuce = "no",
                            Bacon = "yes",
                            CacaramelizedOnion = "no",
                            FriedEgg = "no",
                            RegOnion = "no",
                            Tomato = "no",
                            CheeseType = "cheddar",
                            Sauce = "none"
                        },
                        new
                        {
                            BurgerId = 9,
                            Meat = "beef",
                            Letuce = "no",
                            Bacon = "yes",
                            CacaramelizedOnion = "no",
                            FriedEgg = "yes",
                            RegOnion = "yes",
                            Tomato = "no",
                            CheeseType = "parmesan",
                            Sauce = "cream"
                        },
                        new
                        {
                            BurgerId = 10,
                            Meat = "quadruple beef",
                            Letuce = "yes",
                            Bacon = "yes",
                            CacaramelizedOnion = "no",
                            FriedEgg = "no",
                            RegOnion = "no",
                            Tomato = "yes",
                            CheeseType = "quadruple cheddar",
                            Sauce = "cheese"
                        });
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("OrderAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.OrderItem", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("Bacon")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("BurgerId")
                        .HasColumnType("int");

                    b.Property<string>("CacaramelizedOnion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CheeseType")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("FriedEgg")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Letuce")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Meat")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("RegOnion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Sauce")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tomato")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.TheBurger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Burgers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg1.jpg",
                            Name = "Delicious Mexican",
                            price = 10.98
                        },
                        new
                        {
                            Id = 2,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg2.jpg",
                            Name = "Classic Burger",
                            price = 8.9900000000000002
                        },
                        new
                        {
                            Id = 3,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg3.jpg",
                            Name = "Capra",
                            price = 10.99
                        },
                        new
                        {
                            Id = 4,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg4.jpg",
                            Name = "Cheese Madness",
                            price = 11.5
                        },
                        new
                        {
                            Id = 5,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg5.jpg",
                            Name = "Minimalist",
                            price = 7.8499999999999996
                        },
                        new
                        {
                            Id = 6,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg6.jpg",
                            Name = "Chicken Delight",
                            price = 8.9499999999999993
                        },
                        new
                        {
                            Id = 7,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg7.jpg",
                            Name = "San Francisco",
                            price = 10.800000000000001
                        },
                        new
                        {
                            Id = 8,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg8.jpg",
                            Name = "Full Meat",
                            price = 13.25
                        },
                        new
                        {
                            Id = 9,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg9.jpg",
                            Name = "Carbonara",
                            price = 12.99
                        },
                        new
                        {
                            Id = 10,
                            Image = "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg10.jpg",
                            Name = "NoSurvive",
                            price = 19.989999999999998
                        });
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.BurgerOption", b =>
                {
                    b.HasOne("BurgerAPI.Api.Data.Entities.TheBurger", "Burger")
                        .WithMany("Options")
                        .HasForeignKey("BurgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Burger");
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("BurgerAPI.Api.Data.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("BurgerAPI.Api.Data.Entities.TheBurger", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
