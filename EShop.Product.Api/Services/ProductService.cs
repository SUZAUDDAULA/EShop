using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event;
using EShop.Product.Api.Repositories.Interfaces;
using EShop.Product.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductCreated> AddProduct(CreateProduct product)
        {
            return await _repository.AddProduct(product);
        }

        public async Task<ProductCreated> GetProduct(string ProductId)
        {
            return await _repository.GetProduct(ProductId) ;
        }
    }
}
