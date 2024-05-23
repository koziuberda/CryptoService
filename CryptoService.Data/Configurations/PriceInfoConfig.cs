using CryptoService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoService.Data.Configurations;

public class PriceInfoConfig : IEntityTypeConfiguration<PriceInfoDb>
{
    public void Configure(EntityTypeBuilder<PriceInfoDb> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.LastUpdated)
            .IsRequired();
        
        builder.Property(p => p.CurrencyId)
            .IsRequired();
        
        builder.HasOne(p => p.CryptoCurrency)
            .WithOne(c => c.PriceInfo)
            .HasForeignKey<PriceInfoDb>(p => p.CurrencyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("PriceInfos");
    }
}