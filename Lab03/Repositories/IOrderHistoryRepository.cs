using Lab03.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderHistoryRepository
{
    Task<IEnumerable<OrderHistory>> GetAllAsync();
    Task AddAsync(OrderHistory orderHistory);
}