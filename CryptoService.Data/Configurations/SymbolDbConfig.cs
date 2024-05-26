using CryptoService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoService.Data.Configurations;

public class SymbolDbConfig : IEntityTypeConfiguration<SymbolDb>
{
    public void Configure(EntityTypeBuilder<SymbolDb> builder)
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
        
        builder.Property(p => p.AssetId)
            .IsRequired()
            .HasMaxLength(100);;

        builder.ToTable("Symbols");
    }
}