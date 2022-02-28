using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string Product_Name { get; set; }

        [Required(ErrorMessage = "Product Description is required.")]
        public string Product_Description { get; set; }

        [Required(ErrorMessage = "Product Price is required.")]
        public int Product_Price { get;set; }
        public int Product_Qty { get; set; }
    }
}
