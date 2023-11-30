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
           
            return await _context.Users
                .Where(u => u.Status != "0")
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
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

                user.Status = "0";
                await _context.SaveChangesAsync();
            }
        }


    }

}
