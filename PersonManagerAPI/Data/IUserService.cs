using PersonManagerAPI.Models;

namespace PersonManagerAPI.Data
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}