using EShop.Infrastructure.Command.User;
using EShop.User.DataProvider.Services;
using MassTransit;
using System.Threading.Tasks;

namespace EShop.User.Api.Handlers
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
