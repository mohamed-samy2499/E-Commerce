using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.ProductDTOs
{
    public class ProductAddDTO
    {
        public string? Name { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
