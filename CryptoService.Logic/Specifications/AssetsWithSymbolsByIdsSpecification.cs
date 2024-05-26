using Ardalis.Specification;
using CryptoService.Data.Entities;

namespace CryptoService.Logic.Specifications;

public sealed class AssetsWithSymbolsByIdsSpecification : Specification<AssetDb>
{
    public AssetsWithSymbolsByIdsSpecification(string[] assetIds)
    {
        Query.Where(x => assetIds.Contains(x.Id))
            .Include(x => x.Symbols);
    }
}