using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;

namespace ToolsBazaar.Web.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    public Customer? Update(int customerId, string name)
    {
        return _customerRepository.Update(customerId, name);
    }

    public List<Customer> GetTopFiveCustomersBySpending(DateTime startDate, DateTime endDate)
    {
        var orders = _orderRepository.FindAllByOrderDateBetween(startDate, endDate);
        return orders.GroupBy(order => order.Customer.Id)
            .Select(group => new
            {
                TotalPrice = group.Sum(order => order.CalculateTotalPrice()), group.First().Customer
            })
            .OrderByDescending(_ => _.TotalPrice)
            .Take(5)
            .Select(_ => _.Customer)
            .ToList();
    }
}