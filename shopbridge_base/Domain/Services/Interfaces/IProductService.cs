using Microsoft.AspNetCore.Mvc;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProduct(ProductPagination productPagination);
        Product GetProductById(int id);
        void DeleteProductById(int id);
        void AddProductInInventory(Product product);
        bool IsProductExist(int id);
        void UpdateProductById(Product product);
    }
}
