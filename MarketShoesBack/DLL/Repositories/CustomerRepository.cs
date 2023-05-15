using DLL.Context;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(MarketShoesContext context) : base(context)
        {
        }
    }
}
