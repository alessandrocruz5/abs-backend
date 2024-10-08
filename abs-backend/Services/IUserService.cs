using abs_backend.Models;

namespace abs_backend.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
