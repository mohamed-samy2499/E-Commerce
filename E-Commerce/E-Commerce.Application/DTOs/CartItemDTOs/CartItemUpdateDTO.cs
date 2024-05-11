using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.CartItemDTOs
{
    public class CartItemUpdateDTO
    {
        public int Id { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public float Price { get; set; }

        //Relations
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}
