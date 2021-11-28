using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonManagerAPI.Models;
using PersonManagerAPI.Persistence;

namespace PersonManagerAPI.Data
{
    public class UserRepo : IUserService
    {
        private readonly PersonDbContext _dbContext;

        public UserRepo(PersonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            User first = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));

            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}