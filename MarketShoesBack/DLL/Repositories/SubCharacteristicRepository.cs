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
    public class SubCharacteristicRepository : BaseRepository<SubCharacteristic>
    {
        public SubCharacteristicRepository(MarketShoesContext context) : base(context)
        {
        }


        public override async Task<IEnumerable<SubCharacteristic>> GetAllAsync()
        {
            return await Entities
                .Include(x => x.Characteristic)
                .ToListAsync();
        }

        public override async Task<IEnumerable<SubCharacteristic>> FindByConditionalAsync(Expression<Func<SubCharacteristic, bool>> predicate)
        {
            return await Entities
                .Include(x=>x.Characteristic)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<SubCharacteristic?> UpdeteAsync(SubCharacteristic newSubCharacteristic,int id)
        {
            var subCharacteristic = await FindFirstAsync(x => x.Id == id);
            if (subCharacteristic == null)
                return null;
            subCharacteristic.Name = newSubCharacteristic.Name;

            _context.Entry(subCharacteristic).State = EntityState.Modified;

            await SaveChangesAsync();
            return subCharacteristic;
        }






    }
}
