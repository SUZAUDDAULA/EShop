using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event;
using EShop.Infrastructure.Event.Product;
using EShop.Infrastructure.Query.Product;
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
    public class ProductController : ControllerBase
    {
        private readonly IBusControl _bus;
        private readonly IConfiguration _configuration;
        private readonly IRequestClient<GetProductById> _requestClient;

        public ProductController(IBusControl bus, IConfiguration configuration, IRequestClient<GetProductById> request)
        {
            _bus = bus;
            _requestClient = request;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(string productId)
        {
            try
            {
                var prdct = new GetProductById() { ProductId = productId };
                var product = await _requestClient.GetResponse<ProductCreated>(prdct);
                return Accepted(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProduct product)
        {
            var uri = new Uri("rabbitmq://localhost/create_product");
            var endPoint =await _bus.GetSendEndpoint(uri);
            await endPoint.Send(product);
            return Accepted("Product Created");
        }
    }
}
