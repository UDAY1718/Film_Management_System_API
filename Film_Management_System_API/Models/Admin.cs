using System;
using System.Collections.Generic;

namespace Film_Management_System_API.Models
{
    public partial class Admin
    {
        public string AdminId { get; set; } = null!;
        public string AdminUsername { get; set; } = null!;
        public string AdminEmail { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
    }
}
