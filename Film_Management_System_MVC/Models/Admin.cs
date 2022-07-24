using System.ComponentModel.DataAnnotations;

namespace Film_Management_System_MVC.Models
{
    public class Admin
    {
        public int AdminId { get; set; } 
     
        public string AdminUsername { get; set; } = null!;
        public string AdminEmail { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
    }
}
