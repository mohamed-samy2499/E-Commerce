using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.CartDTOs
{
    public class CartUpdateDTO
    {
        public int Id { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        //The Total Items Price inside the Cart
        public float TotalPrice { get; set; }
        //Relations
        public string? UserId { get; set; }
    }
}
