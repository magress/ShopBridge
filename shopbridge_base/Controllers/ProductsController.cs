using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(IProductService _productService, ILogger<ProductsController> _logger)
        {
            this.productService = _productService;
            this.logger = _logger;
        }

        /// <summary>
        ///Get All Products
        /// </summary>
        [HttpGet]   
        public async Task<IActionResult> GetProduct([FromQuery] ProductPagination productPagination)
        {
            try
            {
                var product = productService.GetProduct(productPagination);
                return Ok(product);
            }
            catch (Exception exception)
            {
              
                logger.LogError(exception, "Error in Product.Get ");
                return StatusCode(500);
            }

        }

        /// <summary>
        ///Get Product by Product Id
        /// </summary>
        /// <param name="product_id">
        /// </param>
        [HttpGet("{product_id}")]
        public async Task<IActionResult> GetProductById(int product_id)
        {
            try
            {
                var isProductIdExist = IsProductExist(product_id);
                if(isProductIdExist)
                {
                    var product = productService.GetProductById(product_id);
                    return Ok(product);
                }


                return BadRequest($"Product with ProductId {product_id} does not exist");


            }
            catch (Exception exception)
            {

                logger.LogError(exception, "Error in Product.Get(id) ");
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Update Existing Product
        /// </summary>
        /// <param name="product">
        /// <b>product_Id (int) :<i> Required</i></b><br/>
        /// <b>Product_Name (string) :<i> Required </i>:</b> Product Name must be less than 100 characters.<br/>
        /// <b>Product_Description (string) :<i> Required </i>:</b> Product Name must be less than 1000 characters.<br/>
        /// <b>Product_Price (float) :<i> Required </i>:</b><br/>     
        /// <b>Product_Qty (int) :<i> Optional </i>:</b>
        /// </param>
        /// <returns>Endpoint for Update Product</returns>
        [HttpPut]
        public async Task<IActionResult> PutProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isProductIdExist = IsProductExist(product.Product_Id);
                if (isProductIdExist)
                {
                    productService.UpdateProductById(product);

                    return Ok($"Product {product.Product_Name} with {product.Product_Id} updated successfully");
                }

                return BadRequest($"Product with ProductId {product.Product_Id} does not exist");

            }
            catch (Exception exception)
            {

                logger.LogError(exception, "Error in Product.PUT ");
                return StatusCode(500);
            }

        }




        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="product">
        /// <b>product_Id (int) :<i> Required</i></b><br/>
        /// <b>Product_Name (string) :<i> Required </i>:</b> Product Name must be less than 100 characters.<br/>
        /// <b>Product_Description (string) :<i> Required </i>:</b> Product Name must be less than 1000 characters.<br/>
        /// <b>Product_Price (float) :<i> Required </i>:</b><br/>     
        /// <b>Product_Qty (int) :<i> Optional </i>:</b>
        /// </param>
        /// <returns>Endpoint for Add Product</returns>
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isProductIdExist = IsProductExist(product.Product_Id);
                if(!isProductIdExist)
                {
                    productService.AddProductInInventory(product);

                    return Ok($"Product {product.Product_Name}  saved successfully");
                }
                return BadRequest($"Product with ProductId {product.Product_Id} already  exist");

            }
            catch (Exception exception)
            {

                logger.LogError(exception, "Error in Product.POST ");
                return StatusCode(500);
            }
        }

        /// <summary>
        ///Delete Product by Product Id
        /// </summary>
        /// <param name="product_id">
        /// </param>
        [HttpDelete("{product_id}")]
        public async Task<IActionResult> DeleteProduct(int product_id)
        {
            try
            {
                var isProductIdExist = IsProductExist(product_id);
                if (isProductIdExist)
                {
                    productService.DeleteProductById(product_id);

                    return Ok($"Product {product_id}  Deleted successfully");
                }
                return BadRequest($"Product with ProductId {product_id} does not  exist");
            }
            catch (Exception exception)
            {

                logger.LogError(exception, "Error in Product.DELETE ");
                return StatusCode(500);
            }

        }

        private bool IsProductExist(int id)
        {
            return productService.IsProductExist(id);
        }
    }
}
