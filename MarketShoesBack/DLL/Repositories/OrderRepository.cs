using DLL.Context;
using DLL.Models;
using DLL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(MarketShoesContext context) : base(context)
        {
        }


        public async override Task<IEnumerable<Order>> GetAllAsync()
        {
            return await Entities
                .Include(x => x.OrderItems)
                .Include(x => x.Seller)
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Order>> FindByConditionalAsync(Expression<Func<Order, bool>> predicate)
        {
            return await Entities
                .Include(x => x.OrderItems)
                .ThenInclude(x=>x.Product)
                .Include(x => x.Seller)
                .Include(x => x.Customer)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Order?> UpdateState(int id,OrderStatus state)
        {
            var order = await FindFirstAsync(x => x.Id == id);
            if(order == null)
                return null;

            order.State = state;
            _context.Entry(order).State = EntityState.Modified;

            await SaveChangesAsync();

            return order;
        }
    }
}
