using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<ProductCreated> AddProduct(CreateProduct product);
        public Task<ProductCreated> GetProduct(Guid ProductId);
    }
}
