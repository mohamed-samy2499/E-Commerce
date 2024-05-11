using E_Commerce.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class Cart:BaseEntity
    {
        //The Total Items Price inside the Cart
        public float TotalPrice { get; set; }

        //Relations
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public List<CartItem> CartItems { get; set; } = [];
    }
}
