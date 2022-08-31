using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using EShop.User.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.User.Api.Services
{
    public class UserService:IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserCreated> AddUser(CreateUser user)
        {
            return await _userRepository.AddUser(user);
        }

        public async Task<UserCreated> GetUser(CreateUser user)
        {
            return await _userRepository.GetUser(user);
        }
    }
}
