using Ardalis.Specification.EntityFrameworkCore;
using CryptoService.Data.Database;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoService.Data.Repositories;

public class AssetRepository : RepositoryBase<AssetDb>, IAssetRepository
{
    public AssetRepository(CryptoDbContext dbContext) : base(dbContext)
    {
    }
}