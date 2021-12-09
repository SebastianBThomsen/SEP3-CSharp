﻿// <auto-generated />
using System;
using DataBaseAccess.DataAccess.DbContextImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataBaseAccess.Migrations
{
    [DbContext(typeof(SEP_DBContext))]
    partial class SEP_DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Entities.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("Length")
                        .HasColumnType("double precision");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Entities.Models.ItemLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("LocationId");

                    b.ToTable("ItemLocations");
                });

            modelBuilder.Entity("Entities.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Entities.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderNumber")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Entities.Models.OrderEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderEntry");
                });

            modelBuilder.Entity("Entities.Models.OrderEntryDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id", "OrderId")
                        .IsUnique();

                    b.ToTable("OrderEntriesDbs");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<int>("SecurityLevel")
                        .HasColumnType("integer");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.Models.ItemLocation", b =>
                {
                    b.HasOne("Entities.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("Entities.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.Navigation("Item");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Entities.Models.OrderEntry", b =>
                {
                    b.HasOne("Entities.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("Entities.Models.Order", null)
                        .WithMany("OrderEntries")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Entities.Models.Order", b =>
                {
                    b.Navigation("OrderEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
