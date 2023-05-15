using BLL.Services.Interfaces;
using DLL.Models;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SellerService : ISellerService, IProductService, ICharacteristicService
    {
        private readonly SellerRepository _sellerRepository;
        private readonly ProductRepository _productRepository;
        private readonly CharacteristicRepository _characteristicRepository;
        private readonly SubCharacteristicRepository _subCharacteristicRepository;

        public SellerService(SellerRepository seller, ProductRepository productRepository, CharacteristicRepository characteristicRepository, SubCharacteristicRepository subCharacteristicRepository)
        {
            _sellerRepository = seller;
            _productRepository = productRepository;
            _characteristicRepository = characteristicRepository;
            _subCharacteristicRepository = subCharacteristicRepository;
        }


        //products
        public async Task<Product?> CreateAsync(Product product, int sellerId)
        {
            product.SellerId = sellerId;
            return await _productRepository.CreateAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(List<SubCharacteristic> subCharacteristics)
        {
            return await _productRepository.FindByConditionalAsync(x => x.Characteristics.Any(y => subCharacteristics.Any(s => s.Name == y.Name)));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int sellerId)
        {
            return await _productRepository.FindByConditionalAsync(x => x.SellerId == sellerId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string name)
        {
            return await _productRepository.FindByConditionalAsync(x => x.Name == name);
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _productRepository.FindFirstAsync(x => x.Id == id);
        }

        public async Task<Product?> ChangeSaleStatusAsync(int productId, bool status)
        {
            return await _productRepository.ChangeAvailableAsync(productId, status);
        }

        public async Task<Product?> UpdateAsync(Product product, int productId)
        {
            return await _productRepository.UpdateAsync(product, productId);
        }


        //Characteristics and subCharacteristic
        public async Task<IEnumerable<Characteristic>> GetAllCharacteristicsAsync()
        {
            return await _characteristicRepository.GetAllAsync();
        }

        public Task<Characteristic?> GetCharacteristicAsync(int id)
        {
            return _characteristicRepository.FindFirstAsync(x => x.Id == id);
        }

        public Task<Characteristic?> GetCharacteristicAsync(string characteristicName)
        {
            return _characteristicRepository.FindFirstAsync(x => x.SubCharacteristics.Any(y => y.Name == characteristicName));
        }

        public Task<Characteristic?> CreateAsync(Characteristic characteristic)
        {
            return _characteristicRepository.CreateAsync(characteristic);
        }

        public Task<Characteristic?> UpdateAsync(Characteristic characteristic, int id)
        {
            return _characteristicRepository.UpdateAsync(characteristic,id);
        }

        public async Task<IEnumerable<SubCharacteristic>> GetSubCharacteristicsAsync(int characteristicId)
        {
            return await _subCharacteristicRepository.FindByConditionalAsync(x=>x.CharacteristicId == characteristicId);
        }

        public async Task<SubCharacteristic?> GetSubCharacteristicAsync(int id)
        {
            return await _subCharacteristicRepository.FindFirstAsync(x=>x.Id == id);
        }

        public async Task<SubCharacteristic?> CreateAsync(SubCharacteristic characteristic)
        {
            return await _subCharacteristicRepository.CreateAsync(characteristic);
        }

        public async Task<SubCharacteristic?> UpdateAsync(SubCharacteristic characteristic, int id)
        {
            return await _subCharacteristicRepository.UpdeteAsync(characteristic, id);
        }
    }
}
