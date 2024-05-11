using E_Commerce.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class CartItem:BaseEntity
    {
        public int Quantity { get; set; }
        public float Price { get; set; }
        //Relations
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CartId { get; set; }
        public Cart? Cart  { get; set; }
    }
}
