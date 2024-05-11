using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.CartItemDTOs
{
    public class CartItemAddDTO
    {
        public int Quantity { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        //Relations
        public int ProductId { get; set; }
        public int CartId { get; set; }

    }
}
