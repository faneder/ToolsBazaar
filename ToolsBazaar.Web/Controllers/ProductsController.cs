using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [Authorize(AuthenticationSchemes = "ApiKey")]
    [HttpGet("most-expensive")]
    public IActionResult GetMostExpensiveProducts()
    {
        _logger.LogInformation("Retrieving the most expensive products...");

        var products = _productRepository.FindAllByPriceDescThenByName();

        return Ok(products);
    }
}