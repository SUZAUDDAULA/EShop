using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using EShop.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.User.DataProvider.Extension
{
    public static class Extension
    {
        public static CreateUser SetPassword(this CreateUser user,IEncrypter encrypter)
        {
            var salt = encrypter.GetSalt();
            user.Password = encrypter.GetHash(user.Password, salt);

            return user;
        }

        public static bool ValidatePassword(this UserCreated user,UserCreated savedUser,IEncrypter encrypter)
        {
            return savedUser.Password.Equals(encrypter.GetHash(user.Password, encrypter.GetSalt()));
        }
    }
}
