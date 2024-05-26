using Ardalis.Specification.EntityFrameworkCore;
using CryptoService.Data.Database;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoService.Data.Repositories;

public class PriceRepository : RepositoryBase<SymbolDb>, IPriceRepository
{
    public PriceRepository(CryptoDbContext dbContext) : base(dbContext)
    {
    }
}