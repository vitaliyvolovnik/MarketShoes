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
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(MarketShoesContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> FindByConditionalAsync(Expression<Func<Product, bool>> predicate)
        {
            return await Entities
                .Include(x=>x.Characteristics)
                .ThenInclude(x=>x.Characteristic)
                .Include(x=>x.Seller)
                .Where(predicate)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Entities
                .Include(x => x.Characteristics)
                .ThenInclude(x => x.Characteristic)
                .ToListAsync();
        }

        public async Task<Product?> UpdateAsync(Product newProduct,int productId)
        {
            var product = await FindFirstAsync(x => x.Id == productId);
            if (product == null)
                return null;

            product.Price = newProduct.Price;
            product.Name = newProduct.Name;
            product.Code = newProduct.Code;
            product.Count = newProduct.Count;

            _context.Entry(product).State = EntityState.Modified;

            await SaveChangesAsync();
            return product;
        }

        public async Task<Product?> ChangeAvailableAsync( int productId, bool isAvailable)
        {
            var product = await FindFirstAsync(x => x.Id == productId);
            if (product == null)
                return null;

            product.IsAvailable = isAvailable;

            _context.Entry(product).State = EntityState.Modified;

            await SaveChangesAsync();
            return product;
        }




    }
}
