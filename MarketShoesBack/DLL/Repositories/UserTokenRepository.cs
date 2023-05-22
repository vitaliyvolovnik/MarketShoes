using DLL.Context;
using DLL.Models;
using DLL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DLL.Repositories
{
    public class UserTokenRepository:BaseRepository<UserToken>
    {
        public UserTokenRepository(MarketShoesContext context) : base(context)
        {
        }

        public async override Task<UserToken?> FindFirstAsync(Expression<Func<UserToken, bool>> predicate)
        {
            return await Entities
                .Include(x => x.User)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<UserToken?> UseAsync(string token, TokenType type)
        {
            var userToken = await Entities
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token && !x.IsUsed && x.type == type);
            if (userToken == null)
                return null;

            userToken.IsUsed = true;

            _context.Entry(userToken).State = EntityState.Modified;
            await SaveChangesAsync();

            return userToken;
        }



    }
}
