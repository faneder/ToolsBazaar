using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Persistence;

public class ProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAll() => DataSet.AllProducts;
    public IEnumerable<Product> FindAllByPriceDescThenByName()
    {
        return DataSet.AllProducts
            .OrderByDescending(p => p.Price)
            .ThenBy(p => p.Name);
    }
}