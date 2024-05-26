using CryptoService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoService.Data.Configurations;

public class AssetDbConfig : IEntityTypeConfiguration<AssetDb>
{
    public void Configure(EntityTypeBuilder<AssetDb> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasMany(c => c.Symbols)
            .WithOne(p => p.Asset)
            .HasForeignKey(p => p.AssetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Assets");
    }
}