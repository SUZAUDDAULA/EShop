using EShop.Infrastructure.Event;
using EShop.Infrastructure.Query.Product;
using EShop.Product.Api.Services.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Query.Api.Handlers
{
    public class GetProductByIdHandlers : IConsumer<GetProductById>
    {
        private readonly IProductService _service;
        public GetProductByIdHandlers(IProductService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<GetProductById> context)
        {
            var product = await _service.GetProduct(context.Message.ProductId);
            await context.RespondAsync<ProductCreated>(product);
        }
    }
}
