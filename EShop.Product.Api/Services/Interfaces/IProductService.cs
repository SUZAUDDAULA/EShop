using EShop.Infrastructure.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductCreated> GetProduct(Guid ProductId);
        Task<ProductCreated> AddProduct(ProductCreated product);
    }
}
