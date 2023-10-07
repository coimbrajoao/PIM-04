using Course.Data;
using Course.Models;
using Microsoft.EntityFrameworkCore;

namespace Course.Repository
{
    public class UserRepository
    {

        private readonly FolhaContext _context;

        public UserRepository(FolhaContext context)
        {
            _context = context;
        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }


    }

}
