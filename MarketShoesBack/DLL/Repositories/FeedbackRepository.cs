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
    public class FeedbackRepository : BaseRepository<Feedback>
    {
        public FeedbackRepository(MarketShoesContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Feedback>> FindByConditionalAsync(Expression<Func<Feedback, bool>> predicate)
        {
            return await Entities
                .Include(x=>x.Product)
                .Include(x=>x.Customer)
                .Where(predicate)
                .ToListAsync();
        }

        public async override Task<Feedback?> FindFirstAsync(Expression<Func<Feedback, bool>> predicate)
        {
            return await Entities
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(predicate);
        }

        public async override Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await Entities
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public async Task<Feedback?> UpdateAsync(Feedback newFeedback,int id)
        {
            var feedback = await FindFirstAsync(x => x.Id == id);

            if(feedback == null) 
                return null;

            feedback.Text = newFeedback.Text;
            feedback.Rating = newFeedback.Rating;

            _context.Entry(feedback).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await SaveChangesAsync();
            return feedback;
        }

        public async Task DeleteAsync(int id)
        {
            var feedbacks = Entities.Where(x => x.Id == id);
            Entities.RemoveRange(feedbacks);

            await SaveChangesAsync();
        }

    }
}
