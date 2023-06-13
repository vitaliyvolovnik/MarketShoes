using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IBasketService
    {


        Task<IEnumerable<BasketItem>> GetBasketAsync(int userId);

        Task<BasketItem?> AddToBasketAsync(BasketItem element,int customerId);

        Task RemoveFromBasketAsync(int basketElementId);

        Task ClearAsync(int customerId);

        Task<BasketItem?> UpdateBusketElement(BasketItem element,int elementId);

    }
}
