using Ardalis.Specification;

namespace CryptoService.Data.Repositories.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    
}