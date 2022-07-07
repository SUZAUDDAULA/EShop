using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Infrastructure.Command.Product
{
    public class CreateProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
        public Guid CategoryId { get; set; }

    }
}
