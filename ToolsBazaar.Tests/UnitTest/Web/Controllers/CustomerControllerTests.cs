using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Web.Controllers;
using ToolsBazaar.Web.Services;

namespace ToolsBazaar.Tests.UnitTest.Web.Controllers;

public class CustomerControllerTests
{
    private readonly ICustomerService _mockCustomerService;
    private readonly ILogger<CustomersController> _logger;

    public CustomerControllerTests()
    {
        _mockCustomerService = Substitute.For<ICustomerService>();
        _logger = Substitute.For<ILogger<CustomersController>>();
    }
    
    [Fact]
    public void GivenCustomerIsNull_WhenUpdatesCustomerName_ReturnsNotFound()
    {
        _mockCustomerService.Update(Arg.Any<int>(), Arg.Any<string>())
            .Returns(_ => null);
        var controller = new CustomersController(_logger, _mockCustomerService);

        var result = controller.UpdateCustomerName(1, new CustomerDto("HappyCoder"));
        
        result.Should().BeOfType<NotFoundResult>();
    }
}
