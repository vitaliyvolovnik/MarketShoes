using DLL.Models;
using DLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order?> CreateOrdersAsync(Order order);
        Task<IEnumerable<Order>> CreateOrdersAsync(int customerId);

        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
        Task<IEnumerable<Order>> GetOrdersBySellerAsync(int sellerId);

        Task<Order?> ChangeOrderState(int id, OrderStatus orderState);


    }
}
