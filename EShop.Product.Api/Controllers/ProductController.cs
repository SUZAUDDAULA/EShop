using EShop.Infrastructure.Command.Product;
using EShop.Product.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> GetProductById(string ProductId)
        {
            var product = await _productService.GetProduct(ProductId);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProduct product)
        {
            var addProduct = await _productService.AddProduct(product);
            return Ok(addProduct);
        }
    }
}
