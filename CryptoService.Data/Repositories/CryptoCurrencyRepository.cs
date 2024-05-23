using Ardalis.Specification.EntityFrameworkCore;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoService.Data.Repositories;

public class CryptoCurrencyRepository : RepositoryBase<CryptoCurrencyDb>, IRepository<CryptoCurrencyDb>
{
    public CryptoCurrencyRepository(DbContext dbContext) : base(dbContext)
    {
    }
}