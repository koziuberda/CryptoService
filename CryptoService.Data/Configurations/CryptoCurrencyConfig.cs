using CryptoService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoService.Data.Configurations;

public class CryptoCurrencyConfig : IEntityTypeConfiguration<CryptoCurrencyDb>
{
    public void Configure(EntityTypeBuilder<CryptoCurrencyDb> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasMany(c => c.Prices)
            .WithOne(p => p.Currency)
            .HasForeignKey(p => p.CurrencyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("CryptoCurrencies");
    }
}