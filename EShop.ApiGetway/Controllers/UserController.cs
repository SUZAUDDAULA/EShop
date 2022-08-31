using EShop.Infrastructure.Command.User;
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
        public UserController(IBusControl bus, IConfiguration configuration)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] CreateUser user)
        {
            var uri = new Uri("rabbitmq://localhost/add_user");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(user);
            return Accepted("User Added");
        }
    }
}
