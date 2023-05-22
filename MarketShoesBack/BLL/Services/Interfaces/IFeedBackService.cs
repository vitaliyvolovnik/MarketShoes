using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IFeedBackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();
        Task<IEnumerable<Feedback>> GetCustomerFeedbacksAsync(int customerId);
        Task<IEnumerable<Feedback>> GetProductFeedbacksAsync(int productId);
        Task<Feedback?> GetFeedbackAsync(int feedbackId);


        Task<Feedback?> CreateAsync(Feedback feedback,int customerId);
        Task<Feedback?> UpdateAsync(Feedback feedback,int feedbackId);
        Task DeleteFeedBackAsync(int feedbackId);

    }
}
