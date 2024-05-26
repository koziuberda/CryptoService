using Ardalis.Specification.EntityFrameworkCore;
using CryptoService.Data.Database;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoService.Data.Repositories;

public class SymbolRepository : RepositoryBase<SymbolDb>, ISymbolRepository
{
    public SymbolRepository(CryptoDbContext dbContext) : base(dbContext)
    {
    }
}