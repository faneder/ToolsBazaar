namespace ToolsBazaar.Domain.CustomerAggregate;

public interface ICustomerRepository
{
    Customer? UpdateCustomerName(int customerId, string name);
    IEnumerable<Customer> GetAll();
}