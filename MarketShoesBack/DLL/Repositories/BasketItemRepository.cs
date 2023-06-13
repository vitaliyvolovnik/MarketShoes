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
    public class BasketItemRepository : BaseRepository<BasketItem>
    {
        private readonly SubCharacteristicRepository _subCharacteristicRepository;
        public BasketItemRepository(MarketShoesContext context,SubCharacteristicRepository subCharacteristicRepository) : base(context)
        {
            _subCharacteristicRepository = subCharacteristicRepository;
        }


        public override async Task<IEnumerable<BasketItem>> FindByConditionalAsync(Expression<Func<BasketItem, bool>> predicate)
        {
            return await Entities
                .Include(x => x.Product)
                .ThenInclude(x=>x.Photos)
                .Where(predicate)
                .ToListAsync();
        }


        public async Task DeleteAsync(Expression<Func<BasketItem, bool>> predicate)
        {
            Entities.RemoveRange(Entities.Where(predicate));
            await SaveChangesAsync();
        }

        public async Task<BasketItem> UpdateAsync(BasketItem newElement,int id)
        {
            var element = await FindFirstAsync(x => x.Id == id);

            if (element == null) return null;

            List<SubCharacteristic> sub = (List<SubCharacteristic>)await _subCharacteristicRepository.FindByConditionalAsync(x => newElement.SubCharacteristics.Any(y => y.Name == x.Name));
            element.SubCharacteristics = sub;
            element.Count = element.Count;


            _context.Entry(element).State = EntityState.Modified;
            await SaveChangesAsync();

            return element;
        }

    }
}
    