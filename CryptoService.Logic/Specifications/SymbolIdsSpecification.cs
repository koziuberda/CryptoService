using Ardalis.Specification;
using CryptoService.Data.Entities;

namespace CryptoService.Logic.Specifications;

public class SymbolIdsSpecification : Specification<SymbolDb, string>
{
    public SymbolIdsSpecification()
    {
        Query.Select(x => x.SymbolId);
    }
}