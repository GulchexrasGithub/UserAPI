using System;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Brokers.Loggings;
using UserAPI.Brokers.Storages;
using UserAPI.Models.Users;

namespace UserAPI.Services.Users
{
    public partial class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<User> AddUserAsync(User user) =>
        TryCatch(async () =>
        {
            ValidateUserOnAdd(user);

            return await this.storageBroker.InsertUserAsync(user);
        });

        public IQueryable<User> RetrieveAllUsers() =>
        TryCatch(() => this.storageBroker.SelectAllUsers());

        public ValueTask<User> ModifyUserAsync(User user) =>
        TryCatch(async () =>
        {
            ValidateUserOnModify(user);
            var maybeUser = await this.storageBroker.SelectUserByIdAsync(user.Id);

            return await this.storageBroker.UpdateUserAsync(user);
        });

        public ValueTask<User> RemoveUserByIdAsync(Guid userId) =>
        TryCatch(async () =>
        {
            ValidateUserId(userId);

            User maybeUser =
                await this.storageBroker.SelectUserByIdAsync(userId);

            ValidateStorageUser(maybeUser, userId);

            return await this.storageBroker.DeleteUserAsync(maybeUser);
        });
    }
}