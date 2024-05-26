using CryptoService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoService.Data.Configurations;

public class PriceInfoConfig : IEntityTypeConfiguration<PriceInfoDb>
{
    public void Configure(EntityTypeBuilder<PriceInfoDb> builder)
    {
        builder.HasKey(p => p.SymbolId);
        
        builder.Property(p => p.SymbolId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.LastUpdated)
            .IsRequired();
        
        builder.Property(p => p.CurrencyId)
            .IsRequired()
            .HasMaxLength(100);;

        builder.ToTable("PriceInfos");
    }
}