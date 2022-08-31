﻿using EShop.Infrastructure.Command.User;
using EShop.User.Api.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.User.Api
{
    public class CreateUserHandler : IConsumer<CreateUser>
    {
        private IUserService _service;
        public CreateUserHandler(IUserService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CreateUser> context)
        {
            var createUser = await _service.AddUser(context.Message);
        }
    }
}
