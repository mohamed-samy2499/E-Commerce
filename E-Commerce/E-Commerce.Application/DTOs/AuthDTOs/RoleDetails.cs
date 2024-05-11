using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.AuthDTOs
{
    public class RoleDetails
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<UserInRoleDTO> Users { get; set; } = [];
    }
}
