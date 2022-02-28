using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IRepository productRepository;
        public ProductService(IRepository _productRepo)
        {
            this.productRepository = _productRepo;
        }

        public void AddProductInInventory(Product product)
        {
            productRepository.AddProductInInventory(product);
        }

        public void DeleteProductById(int id)
        {
            productRepository.DeleteProductById(id);
        }

        public IEnumerable<Product> GetProduct(ProductPagination productPagination)
        {
            return productRepository.GetProduct(productPagination);
        }

        public Product GetProductById(int id)
        {
            return productRepository.GetProductById(id);
        }

        public bool IsProductExist(int id)
        {
            return productRepository.IsProductExist(id);
        }

        public void UpdateProductById(Product product)
        {
            productRepository.UpdateProductById(product);
  
        }
    }
}
