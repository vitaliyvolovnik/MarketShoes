using DLL.Context;
using DLL.Models;


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
        
        public async Task<bool> ConfirmEmail(string email)
        {
            var user = await Entities.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            user.IsEmailConfirm = true;
            await this.SaveChangesAsync();

            return true;
        }


    }
}
