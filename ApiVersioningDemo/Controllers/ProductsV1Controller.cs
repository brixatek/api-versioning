using Microsoft.AspNetCore.Mvc;
using ApiVersioningDemo.Models;
using ApiVersioningDemo.Attributes;

namespace ApiVersioningDemo.Controllers;

[ApiController]
[ApiVersion("1.0", Deprecated = true)]
[Route("api/v{version:apiVersion}/[controller]")]
[Deprecated(DeprecationDate = "2024-06-01", SunsetDate = "2024-12-31", AlternativeVersion = "2.0")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
        new Product { Id = 2, Name = "Book", Price = 19.99m, Category = "Education" }
    };

    /// <summary>
    /// ⚠️ DEPRECATED - Gets all products (V1)
    /// </summary>
    /// <remarks>
    /// 🚨 This endpoint is deprecated and will be removed on 2024-12-31.
    /// Please migrate to /api/v2.0/products
    /// </remarks>
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        var response = new
        {
            _deprecation = new
            {
                warning = "⚠️ This API version is deprecated and will be removed on 2024-12-31",
                migration = "Please migrate to /api/v2.0/products",
                deprecatedSince = "2024-06-01",
                sunsetDate = "2024-12-31"
            },
            data = Products
        };
        return Ok(response);
    }

    /// <summary>
    /// ⚠️ DEPRECATED - Gets a specific product (V1)
    /// </summary>
    /// <remarks>
    /// 🚨 This endpoint is deprecated and will be removed on 2024-12-31.
    /// Please migrate to /api/v2.0/products/{id}
    /// </remarks>
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        
        var response = new
        {
            _deprecation = new
            {
                warning = "⚠️ This API version is deprecated and will be removed on 2024-12-31",
                migration = "Please migrate to /api/v2.0/products/{id}"
            },
            data = product
        };
        return Ok(response);
    }

    /// <summary>
    /// ⚠️ DEPRECATED - Creates a new product (V1)
    /// </summary>
    /// <remarks>
    /// 🚨 This endpoint is deprecated and will be removed on 2024-12-31.
    /// Please migrate to /api/v2.0/products
    /// </remarks>
    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        product.Id = Products.Max(p => p.Id) + 1;
        Products.Add(product);
        
        var response = new
        {
            _deprecation = new
            {
                warning = "⚠️ This API version is deprecated and will be removed on 2024-12-31",
                migration = "Please migrate to /api/v2.0/products"
            },
            data = product
        };
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, response);
    }
}