namespace ToolsBazaar.Domain.CustomerAggregate;

public interface ICustomerRepository
{
    Customer? Update(int customerId, string name);
    IEnumerable<Customer> GetAll();
}