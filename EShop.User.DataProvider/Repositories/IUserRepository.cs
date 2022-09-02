using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.User.DataProvider.Repositories
{
    public interface IUserRepository
    {
        Task<UserCreated> AddUser(CreateUser user);
        Task<UserCreated> GetUser(CreateUser user);
        Task<UserCreated> GetUserByUsername(string userName);
    }
}
