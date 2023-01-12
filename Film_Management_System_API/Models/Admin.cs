using System;
using System.Collections.Generic;

namespace Film_Management_System_API.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string? AdminUsernameEmail { get; set; }
        public string? AdminPassword { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
