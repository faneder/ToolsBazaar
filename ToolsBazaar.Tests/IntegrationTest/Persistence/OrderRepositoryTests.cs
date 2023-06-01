using System.Globalization;
using FluentAssertions;
using ToolsBazaar.Persistence;

namespace ToolsBazaar.Tests.IntegrationTest.Persistence;

public class OrderRepositoryTests
{
    [Fact]
    public void ReturnAllOrders()
    {
        var repository = new OrderRepository();
        
        var result = repository.GetAll();

        result.Count().Should().Be(501);
    }
    
    [Theory]
    [InlineData("1/15/2010", "2/24/2023", 501)]
    [InlineData("1/31/9999", "2/24/2023", 0)]
    [InlineData("1/15/2010", "1/14/2010", 0)]
    public void ReturnAllOrdersByDate(string startDate, string endDate, int expected)
    {
        var repository = new OrderRepository();

        var start = DateTime.ParseExact(startDate, "M/d/yyyy", CultureInfo.InvariantCulture);
        var end = DateTime.ParseExact(endDate, "M/d/yyyy", CultureInfo.InvariantCulture);
        
        var result = repository.FindAllByOrderDateBetween(start, end);

        result.Count().Should().Be(expected);
    }
}