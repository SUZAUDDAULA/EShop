using EShop.Infrastructure.Event;
using EShop.Product.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Services
{
    public class ProductService : IProductService
    {
        public Task<ProductCreated> AddProduct(ProductCreated product)
        {
            return null;
        }

        public Task<ProductCreated> GetProduct(Guid ProductId)
        {
            return null;
        }
    }
}
