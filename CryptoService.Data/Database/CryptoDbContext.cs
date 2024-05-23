﻿using CryptoService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoService.Data.Database;

public class CryptoDbContext : DbContext
{
    public DbSet<CryptoCurrencyDb> CryptoCurrencies { get; set; } = null!;

    public DbSet<PriceInfoDb> PriceInfos { get; set; } = null!;
    
    public CryptoDbContext(DbContextOptions<CryptoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(CryptoDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}