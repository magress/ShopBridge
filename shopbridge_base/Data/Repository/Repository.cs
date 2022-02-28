using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly Shopbridge_Context dbcontext;

        public Repository(Shopbridge_Context _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }

        public void AddProductInInventory(Product product)
        {
            dbcontext.Add(product);
            dbcontext.SaveChanges();
        }

        public void DeleteProductById(int id)
        {
            var product = dbcontext.Product.FirstOrDefault(c => c.Product_Id == id);
             dbcontext.Product.Remove(product);
            dbcontext.SaveChanges();
        }

        public IEnumerable<Product> GetProduct(ProductPagination productPagination)
        {
           return dbcontext.Product.Skip((productPagination.PageNumber - 1) * productPagination.PageSize)
                   .Take(productPagination.PageSize)
                   .ToList(); ;


        }

        public Product GetProductById(int id)
        {
       
            return dbcontext.Product.Where(c => c.Product_Id == id).FirstOrDefault();
        }

        public bool IsProductExist(int id)
        {
            return dbcontext.Product.Any(c => c.Product_Id == id);
       }

        public void UpdateProductById(Product product)
        {
            var existingProduct = dbcontext.Product.First(c => c.Product_Id == product.Product_Id);
           
            existingProduct.Product_Name = product.Product_Name;
            existingProduct.Product_Price = product.Product_Price;
            existingProduct.Product_Qty = product.Product_Qty;
            existingProduct.Product_Description = product.Product_Description;

            dbcontext.Product.Update(existingProduct);

            dbcontext.SaveChanges();
            
          

        }
    }
}
