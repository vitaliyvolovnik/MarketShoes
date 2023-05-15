using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product?> CreateAsync(Product product, int sellerId);
        Task<Product?> UpdateAsync(Product product, int productId);

        Task<Product?> GetProductAsync(int id);


        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsAsync(int sellerId);
        Task<IEnumerable<Product>> GetProductsAsync(List<SubCharacteristic> subCharacteristics);
        Task<IEnumerable<Product>> GetProductsAsync(string name);

        Task<Product?> ChangeSaleStatusAsync(int productId, bool status);


    }
}
