using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopNetCoreApi.Models.Entities
{
    public class Product
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int Id { get; set; }
       public decimal Price { get; set; }
       public int Stock { get; set; }
       public int ViewCount { get; set; }

       public DateTime CreateDate { get; set; }
       public List<OrderDetail> OrderDetails { get; set; }

       public List<Cart> Carts { get; set; }
       public List<ProductImage> ProductImages { get; set; }

    }
}
