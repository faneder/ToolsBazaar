using ToolsBazaar.Domain.CustomerAggregate;

namespace ToolsBazaar.Web.Services;

public interface ICustomerService
{
    Customer? Update(int customerId, string name);
    List<Customer> GetTopFiveCustomersBySpending(DateTime startDate, DateTime endDate);
}