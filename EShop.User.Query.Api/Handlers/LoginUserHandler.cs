using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using EShop.Infrastructure.Security;
using EShop.User.DataProvider.Extension;
using EShop.User.DataProvider.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.User.Query.Api.Handlers
{
    public class LoginUserHandler: IConsumer<LoginUser>
    {
        private UserService _userService;
        private IEncrypter _encrypter;

        public LoginUserHandler(UserService userService,IEncrypter encrypter)
        {
            _userService = userService;
            _encrypter = encrypter;
        }

        public async Task Consume(ConsumeContext<LoginUser> context)
        {
            var userResult = new UserCreated();
            var user = await _userService.GetUserByUsername(context.Message.Username);
            if(user.UserId !=null)
            {
                var isAllowed = user.ValidatePassword(user,_encrypter);
                if (isAllowed)
                    userResult = user;
            }

            await context.RespondAsync<UserCreated>(userResult);
        }
    }
}
