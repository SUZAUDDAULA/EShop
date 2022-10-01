using EShop.Infrastructure.Command.User;
using EShop.Infrastructure.Event.User;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ApiGetway.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBusControl _bus;
        private IRequestClient<LoginUser> _loginRequestClient;
        public UserController(IBusControl bus, IConfiguration configuration, IRequestClient<LoginUser> loginRequestClient)
        {
            _bus = bus;
            _loginRequestClient = loginRequestClient;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] CreateUser user)
        {
            var uri = new Uri("rabbitmq://localhost/add_user");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(user);
            return Accepted("User Added");
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUser loginUser)
        {
            var userResponse = await _loginRequestClient.GetResponse<UserCreated>(loginUser);
            return Accepted(userResponse.Message);
        }

        public IActionResult GetInfo()
        {
            string result = "";
            return Ok(result);
        }
    }
}
