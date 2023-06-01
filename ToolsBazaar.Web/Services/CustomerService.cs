using ToolsBazaar.Domain.CustomerAggregate;

namespace ToolsBazaar.Web.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer? Update(int customerId, string name)
    {
        return _customerRepository.Update(customerId, name);
    }
}