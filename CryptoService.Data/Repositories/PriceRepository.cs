using Ardalis.Specification.EntityFrameworkCore;
using CryptoService.Data.Entities;
using CryptoService.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoService.Data.Repositories;

public class PriceRepository : RepositoryBase<PriceInfoDb>, IRepository<PriceInfoDb>
{
    public PriceRepository(DbContext dbContext) : base(dbContext)
    {
    }
}