using EShop.Infrastructure.Command.Product;
using EShop.Product.Api.Services.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace EShop.Product.Api.Handlers
{
    public class CreateProductHandler:IConsumer<CreateProduct>
    {
        private readonly IProductService _Service;

        public CreateProductHandler(IProductService service)
        {
            _Service = service;
        }

        public async Task Consume(ConsumeContext<CreateProduct> context)
        {
            await _Service.AddProduct(context.Message);
            await Task.CompletedTask;
        }
    }
}
