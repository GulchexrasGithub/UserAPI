using Microsoft.EntityFrameworkCore;
using UserAPI.Models.Users;

namespace UserAPI.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }
    }
}