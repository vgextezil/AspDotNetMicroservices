using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace Catalog.API.Controllers;
using Swashbuckle.AspNetCore.Annotations;


[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _repository.GetProducts();
        return Ok(products);
    }
    
    /// <summary>
    /// Get product by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:length(24)}", Name = "GetProductById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        var product = await _repository.GetProductById(id);
        if (product == null)
        {
            _logger.LogError($"Product with this id: {id}, not found");
            return NotFound();
        }

        return Ok(product);
    }
    
    /// <summary>
    /// Get product By Category Name
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
    {
        var products = await _repository.GetProductByCategory(category);
        if (products == null)
        {
            _logger.LogError($"Products with this category: {category}, not found");
            return NotFound();
        }
        return Ok(products);
    }
    
    /// <summary>
    /// Add Product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        await _repository.CreateProduct(product);
        return CreatedAtAction("GetProductById", new { id = product.Id }, product);
    }
    
    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        return Ok(await _repository.UpdateProduct(product));
    }
    /// <summary>
    /// Delete Product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
        return Ok(await _repository.DeleteProduct(id));
    }
}