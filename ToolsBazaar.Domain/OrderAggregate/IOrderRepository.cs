namespace ToolsBazaar.Domain.OrderAggregate;

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    IEnumerable<Order> FindAllByOrderDateBetween(DateTime startDate, DateTime endDate);
}