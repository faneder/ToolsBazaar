using FluentAssertions;
using NSubstitute;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Web.Services;

namespace ToolsBazaar.Tests.UnitTest.Web.Services;

public class CustomerServiceTest
{
    private const string Name = "HappyCoder";
    private readonly ICustomerRepository _mockCustomerRepository;

    public CustomerServiceTest()
    {
        _mockCustomerRepository = Substitute.For<ICustomerRepository>();
    }
    
    [Fact]
    public void GivenCustomerIsNull_WhenUpdatesCustomerName_ReturnsNull()
    {
        _mockCustomerRepository.Update(Arg.Any<int>(), Arg.Any<string>())
            .Returns(_ => null);
        var service = new CustomerService(_mockCustomerRepository);

        var result = service.Update(Arg.Any<int>(), Arg.Any<string>());

        result.Should().BeNull();
    }
    
    [Fact]
    public void GivenCustomer_WhenUpdatesCustomerName_ReturnsACustomer()
    {
        _mockCustomerRepository.Update(Arg.Any<int>(), Arg.Any<string>())
            .Returns(new Customer { Name = Name });
        var service = new CustomerService(_mockCustomerRepository);

        var result = service.Update(1, Name);

        result?.Name.Should().Be(Name);
    }
}