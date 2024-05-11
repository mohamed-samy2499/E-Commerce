using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.CartDTOs
{
    public class CartAddDTO
    {
        //The Total Items Price inside the Cart
        public float TotalPrice { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


        //Relations
        public string? UserId { get; set; }
    }
}
