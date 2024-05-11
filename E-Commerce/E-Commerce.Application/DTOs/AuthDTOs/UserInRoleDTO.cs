using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.AuthDTOs
{
    public class UserInRoleDTO
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }

        public bool IsSelected { get; set; }
    }
}
