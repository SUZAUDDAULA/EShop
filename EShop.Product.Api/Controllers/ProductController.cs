﻿using EShop.Infrastructure.Command.Product;
using EShop.Product.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Get(Guid ProductId)
        {
            var product = await _productService.GetProduct(ProductId);
            return Ok(product);
        }

        public async Task<IActionResult> Add([FromForm] CreateProduct product)
        {
            var addProduct = await _productService.AddProduct(product);
            return Ok(addProduct);
        }
    }
}
