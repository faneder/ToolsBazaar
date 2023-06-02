using FluentAssertions;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Tests.UnitTest.Domain.OrderAggregate;

public class OrderTest
{
    [Fact]
    public void CalculateTotalPrice()
    {
        var orderItem1 = new OrderItem{ Quantity = 1, Product = new Product { Price = 10 }};
        var orderItem2 = new OrderItem{ Quantity = 2, Product = new Product { Price = 50 }};
        var order = new Order{Items = new List<OrderItem> { orderItem1, orderItem2 }};

        var totalPrice = order.CalculateTotalPrice();

        totalPrice.Should().Be(110);
    }
}