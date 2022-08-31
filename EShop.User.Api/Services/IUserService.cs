﻿using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.User.Api.Services
{
    public interface IUserService
    {
        Task<UserCreated> AddUser(CreateUser user);
        Task<UserCreated> GetUser(CreateUser user);
    }
}
