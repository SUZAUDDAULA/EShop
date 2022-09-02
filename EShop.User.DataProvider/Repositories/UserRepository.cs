using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.User.DataProvider.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoDatabase _database;
        //private IMongoCollection<CreateUser> _collection => _database.GetCollection<CreateUser>("user");
        private IMongoCollection<CreateUser> _collection;
        public UserRepository(IMongoDatabase database)
        {
            _database = database;
            _collection=database.GetCollection<CreateUser>("user");
        }

        public async Task<UserCreated> AddUser(CreateUser user)
        {
            await _collection.InsertOneAsync(user);
            return new UserCreated()
            {
                ContactNo = user.ContactNo,
                EmailId = user.EmailId,
                Password = user.Password,
                UserName = user.UserName
            };
        }

        public async Task<UserCreated> GetUser(CreateUser user)
        {
            //var userResult = await _collection.AsQueryable().FirstOrDefaultAsync(usr => usr.Username == user.Username);
              var userResult = _collection.AsQueryable().Where(usr=>usr.UserName==user.UserName).FirstOrDefault();
            await Task.CompletedTask;
            if (userResult == null)
                return new UserCreated();

            return new UserCreated()
            {
                UserName=userResult.UserName,
                ContactNo=userResult.ContactNo,
                EmailId=userResult.EmailId,
                Password=userResult.Password,
                UserId=userResult.UserId
            };
        }

        public async Task<UserCreated> GetUserByUsername(string userName)
        {
            //var userResult = await _collection.AsQueryable().FirstOrDefaultAsync(usr => usr.Username == userName);
            var userResult = _collection.AsQueryable().Where(usr => usr.UserName == userName).FirstOrDefault();
            await Task.CompletedTask;
            if (userResult == null)
                return new UserCreated();

            return new UserCreated()
            {
                UserName = userResult.UserName,
                ContactNo = userResult.ContactNo,
                EmailId = userResult.EmailId,
                Password = userResult.Password,
                UserId = userResult.UserId
            };
        }
    }
}
