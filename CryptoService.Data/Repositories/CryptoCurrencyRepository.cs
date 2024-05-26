using Ardalis.Specification.EntityFrameworkCore;
using CryptoService.Data.Database;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoService.Data.Repositories;

public class CryptoCurrencyRepository : RepositoryBase<CryptoCurrencyDb>, ICryptoCurrencyRepository
{
    public CryptoCurrencyRepository(CryptoDbContext dbContext) : base(dbContext)
    {
    }
}