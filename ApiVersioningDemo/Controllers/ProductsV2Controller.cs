using Microsoft.AspNetCore.Mvc;
using ApiVersioningDemo.Models;

namespace ApiVersioningDemo.Controllers;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsV2Controller : ControllerBase
{
    private static readonly List<ProductV2> ProductsV2 = new()
    {
        new ProductV2 
        { 
            Id = 1, 
            Name = "Laptop", 
            Price = 999.99m, 
            Category = "Electronics",
            Description = "High-performance gaming laptop",
            CreatedDate = DateTime.Now.AddDays(-30),
            IsActive = true
        },
        new ProductV2 
        { 
            Id = 2, 
            Name = "Book", 
            Price = 19.99m, 
            Category = "Education",
            Description = "Programming fundamentals guide",
            CreatedDate = DateTime.Now.AddDays(-15),
            IsActive = true
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<ProductV2>> GetProducts([FromQuery] bool includeInactive = false)
    {
        var products = includeInactive ? ProductsV2 : ProductsV2.Where(p => p.IsActive);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductV2> GetProduct(int id)
    {
        var product = ProductsV2.FirstOrDefault(p => p.Id == id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public ActionResult<ProductV2> CreateProduct(ProductV2 product)
    {
        product.Id = ProductsV2.Max(p => p.Id) + 1;
        product.CreatedDate = DateTime.Now;
        ProductsV2.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPatch("{id}/status")]
    public ActionResult UpdateProductStatus(int id, [FromBody] bool isActive)
    {
        var product = ProductsV2.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        
        product.IsActive = isActive;
        return NoContent();
    }
}