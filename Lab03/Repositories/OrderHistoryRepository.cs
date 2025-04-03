using Lab03.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OrderHistoryRepository : IOrderHistoryRepository
{
    private readonly List<OrderHistory> _orderHistories = new();

    public Task<IEnumerable<OrderHistory>> GetAllAsync()
    {
        return Task.FromResult(_orderHistories.AsEnumerable());
    }

    public Task AddAsync(OrderHistory orderHistory)
    {
        orderHistory.Id = _orderHistories.Count + 1;
        _orderHistories.Add(orderHistory);
        return Task.CompletedTask;
    }
}