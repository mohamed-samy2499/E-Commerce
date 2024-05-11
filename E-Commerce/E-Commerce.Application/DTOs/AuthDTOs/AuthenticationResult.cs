using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.AuthDTOs
{
    public class AuthenticationResult
    {
        public bool Succeeded { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = [];
        public string? Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public List<string> Errors { get; set; } = [];
    }
}
