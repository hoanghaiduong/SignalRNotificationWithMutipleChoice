using Microsoft.EntityFrameworkCore;
using myapp.Data;
using myapp.Models;

namespace myapp.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserDetails(string username,string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x=>x.UserName==username && x.Password==password);
        }
    }
}
