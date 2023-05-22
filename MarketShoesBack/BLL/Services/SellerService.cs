using BLL.Services.Interfaces;
using DLL.Models;
using DLL.Models.Enums;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SellerService : ISellerService, IProductService, ICharacteristicService,IOrderService
    {

        private readonly ProductRepository _productRepository;
        private readonly CharacteristicRepository _characteristicRepository;
        private readonly SubCharacteristicRepository _subCharacteristicRepository;
        private readonly OrderRepository _orderRepository;
        private readonly UserRepository _userRepository;

        public SellerService( 
            ProductRepository productRepository, 
            CharacteristicRepository characteristicRepository, 
            SubCharacteristicRepository subCharacteristicRepository, 
            OrderRepository orderRepository,
            UserRepository userRepository)
        {
            _productRepository = productRepository;
            _characteristicRepository = characteristicRepository;
            _subCharacteristicRepository = subCharacteristicRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }


        //products
        public async Task<Product?> CreateAsync(Product product, int uesrId)
        {
            product.SellerId = uesrId;
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


        //Orders
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public Task<Order?> CreateOrdersAsync(Order order)
        {
           return _orderRepository.CreateAsync(order);
        }

        public async Task<IEnumerable<Order>> CreateOrdersAsync(int customerId)
        {
            var user = await _userRepository.FindFirstAsync(x => x.Id == customerId);

            var grouped = user.Basket.GroupBy(x => x.Product.SellerId);
            var orders = new List<Order>(); 

            foreach(var group in grouped)
            {
                var sellerId = group.Key;
                var orderItems = group.Select(x => new OrderItem()
                {
                    Product = x.Product,
                    Count = x.Count,
                    SubCharacteristics = x.SubCharacteristics
                });
                var order = new Order()
                {
                    OrderItems = (List<OrderItem>)orderItems,
                    SellerId = sellerId,
                    CustomerId = customerId,
                    State = OrderStatus.INLINE
                };
                var created = await _orderRepository.CreateAsync(order);
                if (created != null)
                    orders.Add(created);
            }
            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _orderRepository.FindByConditionalAsync(x=>x.CustomerId == customerId);
        }

        public async Task<IEnumerable<Order>> GetOrdersBySellerAsync(int sellerId)
        {
            return await _orderRepository.FindByConditionalAsync(x => x.SellerId == sellerId);
        }

        public async Task<Order?> ChangeOrderState(int id, OrderStatus orderStatus)
        {
            return await _orderRepository.UpdateState(id, orderStatus);
        }
    }
}
