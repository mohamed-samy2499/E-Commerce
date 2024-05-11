using E_Commerce.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class Product:BaseEntity
    {
        public required string Name { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }

        //Relations
        public List<CartItem> CartItems { get; set; } = [];


    }
}
