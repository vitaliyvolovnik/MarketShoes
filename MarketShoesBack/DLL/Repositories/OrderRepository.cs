using DLL.Context;
using DLL.Models;
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

        public override async Task<IEnumerable<Order>> FindByConditionalAsync(Expression<Func<Order, bool>> predicate)
        {
            return await Entities
                .Include(x => x.BasketElement)
                .Include(x => x.Customer)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
