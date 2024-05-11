using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Settings
{
    public static class JwtSettings
    {
        public const string Key = "sz8eI7OdHBrjrIo8j9nTW/rQyO1OvY0pAQ2wDKQZw/0=";
        public const string Issuer = "SecureApi";
        public const string Audience = "SecureApiUser";
        public const int DurationInDays = 30;
    }
}
