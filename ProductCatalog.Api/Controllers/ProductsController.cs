using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductDbContext _context;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ProductDbContext context, ILogger<ProductsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        _logger.LogInformation("Getting product with ID: {ProductId}", id);
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            _logger.LogWarning("Product with ID: {ProductId} not found", id);
            return NotFound();
        }
        return product;
    }

    // GET: api/products.category/Electronics
    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
    {
        _logger.LogInformation("Getting products in category: {Category}", category);
        var products = await _context.Products
            .Where(p => p.Category == category)
            .ToListAsync();

        return products;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _logger.LogInformation("Creating new product: {ProductName}", product.Name);

        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _logger.LogInformation("Updating product with ID: {ProductId}", id);

        product.UpdatedAt = DateTime.UtcNow;
        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        _logger.LogInformation("Deleting product with ID: {ProductId}", id);

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}