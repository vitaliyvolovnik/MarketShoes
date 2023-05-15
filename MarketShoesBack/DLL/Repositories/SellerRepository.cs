using DLL.Context;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class SellerRepository : BaseRepository<Seller>
    {
        public SellerRepository(MarketShoesContext context) : base(context)
        {
        }
    }
}
