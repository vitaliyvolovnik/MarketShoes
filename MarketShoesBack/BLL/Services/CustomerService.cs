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
    public class CustomerService : IFeedBackService, IBasketService
    {

        private readonly UserRepository _userRepository;
        private readonly BasketItemRepository _basketElementRepository;
        private readonly FeedbackRepository _feedbackRepository;


        public CustomerService(BasketItemRepository basketElementRepository, 
            UserRepository userRepository, FeedbackRepository feedbackRepository)
        {
            _basketElementRepository = basketElementRepository;
            _userRepository = userRepository;
            _feedbackRepository = feedbackRepository;
        }
        //Basket and Basket Element
        public async Task<BasketItem?> AddToBasketAsync(BasketItem element, int customerId)
        {
           element.CustomerId = customerId;
            return await _basketElementRepository.CreateAsync(element);
        }
        public async Task<BasketItem?> AddToBasketAsync(int productId, int customerId)
        {
            var basketItem = new BasketItem() { ProductId = productId, CustomerId = customerId, Count = 1};
            return await _basketElementRepository.CreateAsync(basketItem);
        }

        public async Task ClearAsync(int userId)
        {
           await _basketElementRepository.DeleteAsync(x=>x.CustomerId == userId);
        }

        public async Task RemoveFromBasketAsync(int basketElementId)
        {
            await _basketElementRepository.DeleteAsync(x => x.Id == basketElementId);
        }

        public async Task<BasketItem?> UpdateBusketElement(BasketItem element, int elementId)
        {
            return await _basketElementRepository.UpdateAsync(element, elementId);
        }

        //Feedbacks
        public async Task<Feedback?> CreateAsync(Feedback feedback, int customerId)
        {
            feedback.CustomerId = customerId;
            return await _feedbackRepository.CreateAsync(feedback);
        }
        public async Task DeleteFeedBackAsync(int feedbackId)
        {
            await _feedbackRepository.DeleteAsync(feedbackId);
        }
        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _feedbackRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Feedback>> GetCustomerFeedbacksAsync(int customerId)
        {
            return await _feedbackRepository.FindByConditionalAsync(x=>x.CustomerId == customerId);
        }
        public async Task<Feedback?> GetFeedbackAsync(int feedbackId)
        {
            return await _feedbackRepository.FindFirstAsync(x => x.Id == feedbackId);
        }
        public async Task<IEnumerable<Feedback>> GetProductFeedbacksAsync(int productId)
        {
            return await _feedbackRepository.FindByConditionalAsync(x => x.ProductId == productId);   
        }
        public async Task<Feedback?> UpdateAsync(Feedback feedback, int feedbackId)
        {
            return await _feedbackRepository.UpdateAsync(feedback, feedbackId);
        }

        public Task<IEnumerable<BasketItem>> GetBasketAsync(int userId)
        {
                return _basketElementRepository.FindByConditionalAsync(x=>x.CustomerId == userId);
        }
    }
}
