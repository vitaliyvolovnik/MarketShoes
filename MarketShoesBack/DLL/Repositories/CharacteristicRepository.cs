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
    public class CharacteristicRepository : BaseRepository<Characteristic>
    {
        public CharacteristicRepository(MarketShoesContext context) : base(context)
        {
        }


        public override async Task<IEnumerable<Characteristic>> FindByConditionalAsync(Expression<Func<Characteristic, bool>> predicate)
        {
            return await Entities
                .Include(x=>x.SubCharacteristics)
                .Where(predicate)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Characteristic>> GetAllAsync()
        {
            return await Entities
                .Include(x => x.SubCharacteristics)
                .ToListAsync();       
        }

        public override async Task<Characteristic?> FindFirstAsync(Expression<Func<Characteristic, bool>> predicate)
        {
            return await Entities.Include(x => x.SubCharacteristics).FirstOrDefaultAsync(predicate);
        }

        public async Task<Characteristic?> UpdateAsync(Characteristic newCharacteristic, int id)
        {
            var characteristic = await FindFirstAsync(x => x.Id == id);
            if (characteristic == null)
                return null;
            characteristic.Name = newCharacteristic.Name;

            _context.Entry(characteristic).State = EntityState.Modified;

            await SaveChangesAsync();
            return characteristic;

        }


    }
}
