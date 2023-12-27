using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly ICatalogContext _catalogContext;
    
    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _catalogContext.Products.Find(p => true).ToListAsync();
    }

    public Task<Product> GetProduct(string id)
    {
        FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.ElemMatch(p => p.Id, id);
        
    }

    public Task<IEnumerable<Product>> GetProductByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductByCategory(string category)
    {
        throw new NotImplementedException();
    }

    public Task CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(Product product)
    {
        throw new NotImplementedException();
    }
}