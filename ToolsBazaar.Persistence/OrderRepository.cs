using ToolsBazaar.Domain.OrderAggregate;

namespace ToolsBazaar.Persistence;

public class OrderRepository : IOrderRepository
{
    public IEnumerable<Order> FindAllByOrderDateBetween(DateTime startDate, DateTime endDate)
    {
        return DataSet.AllOrders.Where(order => order.Date >= startDate && order.Date <= endDate).ToList(); 
    }
}