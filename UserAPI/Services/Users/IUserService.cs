using System;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Models.Users;

namespace UserAPI.Services.Users
{
    public interface IUserService
    {
        ValueTask<User> AddUserAsync(User user);
        IQueryable<User> RetrieveAllUsers();
        ValueTask<User> ModifyUserAsync(User user);
        ValueTask<User> RemoveUserByIdAsync(Guid userId);
    }
}