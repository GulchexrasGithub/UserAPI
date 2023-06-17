﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models.Users;

namespace UserAPI.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User user) =>
         await InsertAsync(user);

        public IQueryable<User> SelectAllUsers() =>
            SelectAll<User>();

        public async ValueTask<User> UpdateUserAsync(User user) =>
            await UpdateAsync(user);

        public async ValueTask<User> DeleteUserAsync(User user) =>
            await DeleteAsync(user);
    }
}