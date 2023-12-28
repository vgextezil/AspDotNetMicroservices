using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Bson;
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
        return await _catalogContext.Products.Find(product => true).ToListAsync();
    }

    public async Task<Product> GetProductById(string id)
    {
        return await _catalogContext.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(product => product.Name,
            name);
        return await _catalogContext.Products.Find(filterDefinition).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        FilterDefinition<Product> filterDefinition =
            Builders<Product>.Filter.Eq(product => product.Category, categoryName);
        return await _catalogContext.Products.Find(filterDefinition).ToListAsync();

    }

    public async Task CreateProduct(Product product)
    {
        product.Id = ObjectId.GenerateNewId().ToString();
        await _catalogContext.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult =
            await _catalogContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
    

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(product => product.Id, id);
        DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filterDefinition);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}