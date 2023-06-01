﻿using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Web.Services;

namespace ToolsBazaar.Web.Controllers;

public record CustomerDto(string Name);

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpPut("{customerId:int}")]
    public IActionResult UpdateCustomerName([FromRoute] int customerId, [FromBody] CustomerDto dto)
    {
        _logger.LogInformation($"Updating customer #{customerId} name...");

        var customer = _customerService.Update(customerId, dto.Name);

        if (customer == null)
        {
            _logger.LogWarning($"Customer id #{customerId} not found!");
            return NotFound();
        }

        return NoContent();
    }
}