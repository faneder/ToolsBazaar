using FluentAssertions;
using NSubstitute;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.ProductAggregate;
using ToolsBazaar.Web.Services;

namespace ToolsBazaar.Tests.UnitTest.Web.Services;

public class CustomerServiceTest
{
    private const string Name = "HappyCoder";
    private readonly ICustomerRepository _mockCustomerRepository;
    private readonly IOrderRepository _mockOrderRepository;

    public CustomerServiceTest()
    {
        _mockCustomerRepository = Substitute.For<ICustomerRepository>();
        _mockOrderRepository = Substitute.For<IOrderRepository>();
    }
    
    [Fact]
    public void GivenCustomerIsNull_WhenUpdatesCustomerName_ReturnsNull()
    {
        _mockCustomerRepository.Update(Arg.Any<int>(), Arg.Any<string>())
            .Returns(_ => null);
        var service = new CustomerService(_mockCustomerRepository, _mockOrderRepository);

        var result = service.Update(Arg.Any<int>(), Arg.Any<string>());

        result.Should().BeNull();
    }
    
    [Fact]
    public void GivenCustomer_WhenUpdatesCustomerName_ReturnsACustomer()
    {
        _mockCustomerRepository.Update(Arg.Any<int>(), Arg.Any<string>())
            .Returns(new Customer { Name = Name });
        var service = new CustomerService(_mockCustomerRepository, _mockOrderRepository);

        var result = service.Update(1, Name);

        result?.Name.Should().Be(Name);
    }
    
    [Fact]
    public void FilterCustomers_ReturnsTopFiveCustomersSortedBySpendingDesc()
    {
        var order2 = CreateOrder(2, 20);
        var order3 = CreateOrder(3, 30);
        var order4 = CreateOrder(4, 40);
        var order5 = CreateOrder(5, 50);
        var order6 = CreateOrder(6, 60);
        var orders = new List<Order> { CreateOrder(1, 10), order2, order3, order4, order5, order6 };

        _mockOrderRepository.FindAllByOrderDateBetween(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(orders);
        
        var service = new CustomerService(_mockCustomerRepository, _mockOrderRepository);
        var result = service.GetTopFiveCustomersBySpending(new DateTime(1000, 1, 1), new DateTime(9999, 9, 9));

        result.Count.Should().Be(5);
        result.Should().BeEquivalentTo(new[] { order6.Customer, order5.Customer, order4.Customer, order3.Customer, order2.Customer });
    }

    [Fact]
    public void GivenTwoCustomersWithMultipleOrders_ReturnsTwoCustomer()
    {
        var order1 = CreateOrder(1, 100);
        var order2 = CreateOrder(2, 50);
        var order3 = CreateOrder(2, 100);
        
        var orders = new List<Order> { order1, order2, order3};

        _mockOrderRepository.FindAllByOrderDateBetween(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(orders);
        
        var service = new CustomerService(_mockCustomerRepository, _mockOrderRepository);
        var result = service.GetTopFiveCustomersBySpending(new DateTime(1000, 1, 1), new DateTime(9999, 9, 9));

        result.Count.Should().Be(2);
        result.Should().BeEquivalentTo(new[] { order2.Customer, order1.Customer });
    }

    [Fact]
    public void GivenUnderFiveCustomers_ReturnCustomers()
    {
        var order1 = CreateOrder(1, 10);
        var order2 = CreateOrder(2, 20);
        var orders = new List<Order> { order1, order2 };

        _mockOrderRepository.FindAllByOrderDateBetween(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(orders);
        
        var service = new CustomerService(_mockCustomerRepository, _mockOrderRepository);
        var result = service.GetTopFiveCustomersBySpending(new DateTime(1000, 1, 1), new DateTime(9999, 9, 9));

        result.Count.Should().Be(2);
        result.Should().BeEquivalentTo(new[] { order2.Customer, order1.Customer });
    }

    private static Order CreateOrder(int id, int price)
    {
        var order = new Order
        {
            Id = id,
            Date = new DateTime(2019, 5, 10),
            Items = new List<OrderItem> { new() { Quantity = 1, Product = new Product { Price = price}}},
            Customer = new Customer { Id = id, Name = $"Creator{id}", Email = $"creator{id}@happy.com", Address = "outerspace"}
        };
        return order;
    }
}