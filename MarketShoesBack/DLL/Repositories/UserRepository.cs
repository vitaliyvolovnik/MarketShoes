using DLL.Context;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(MarketShoesContext context) : base(context)
        {
        }

        public bool IsEmailExist(string Email)
        {
            return Entities.Any(x => x.Email == Email);
        }   
        
        public async Task<bool> ChangePassworndAsync(string email,string password,string newPassword)
        {
            var user = await Entities.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
                return false;

            user.Password = newPassword;

            await this.SaveChangesAsync();
            
            return true;
        }

        public async Task<User?> LoginAsync(string email, string passwordHash)
        {
            return await Entities.FirstOrDefaultAsync(u => u.Email == email && u.Password == passwordHash /*&& u.IsEmailConfirm*/);
        }

        public async Task<User?> GetWithBasket(Expression<Func<User, bool>> predicate)
        {
            return Entities
                .Include(x=>x.Basket)
                .ThenInclude(x=>x.Product)
                .Include(x=>x.Basket)
                .ThenInclude(x=>x.SubCharacteristics)
                .FirstOrDefault(predicate);
        }

        public async Task<bool> ResetPasswordAsync(int id, string password)
        {
            var user = await Entities.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return false;

            user.Password = password;
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmEmailAsync(int id)
        {
            var user = await Entities.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return false;

            user.IsEmailConfirm = true;
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await Entities.FirstOrDefaultAsync(u => u.Email.Equals(email)) != null;
        }
    }
}
