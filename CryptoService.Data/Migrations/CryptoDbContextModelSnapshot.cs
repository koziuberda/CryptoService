﻿// <auto-generated />
using System;
using CryptoService.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoService.Data.Migrations
{
    [DbContext(typeof(CryptoDbContext))]
    partial class CryptoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CryptoService.Data.Entities.AssetDb", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Assets", (string)null);
                });

            modelBuilder.Entity("CryptoService.Data.Entities.SymbolDb", b =>
                {
                    b.Property<string>("SymbolId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AssetId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(12,6)");

                    b.HasKey("SymbolId");

                    b.HasIndex("AssetId");

                    b.ToTable("Symbols", (string)null);
                });

            modelBuilder.Entity("CryptoService.Data.Entities.SymbolDb", b =>
                {
                    b.HasOne("CryptoService.Data.Entities.AssetDb", "Asset")
                        .WithMany("Symbols")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("CryptoService.Data.Entities.AssetDb", b =>
                {
                    b.Navigation("Symbols");
                });
#pragma warning restore 612, 618
        }
    }
}
