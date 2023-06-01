using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.CustomerAggregate;

namespace ToolsBazaar.Web.Controllers;

public record CustomerDto(string Name);

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    [HttpPut("{customerId:int}")]

    public IActionResult UpdateCustomerName([FromRoute] int customerId, [FromBody] CustomerDto dto)
    {
        _logger.LogInformation($"Updating customer #{customerId} name...");

        var customer = _customerRepository.UpdateCustomerName(customerId, dto.Name);

        if (customer == null)
        {
            _logger.LogWarning($"Customer id #{customerId} not found!");
            return NotFound();
        }

        return NoContent();
    }
}