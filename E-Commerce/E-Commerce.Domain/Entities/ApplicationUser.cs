using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        //Relations 
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
