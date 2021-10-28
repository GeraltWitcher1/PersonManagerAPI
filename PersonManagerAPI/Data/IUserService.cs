using System.Threading.Tasks;
using PersonManagerAPI.Models;

namespace PersonManagerAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string username, string password);
    }
}