using System.ComponentModel.DataAnnotations;

namespace Film_Management_System_API.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Username should not be empty")]
        public string AdminUsernameEmail { get; set; }
        [Required(ErrorMessage = "Password should not be empty")]
        public string AdminPassword { get; set; }   
        public byte[] AdminPasswordHash { get; set; }
        public byte[] AdminPasswordSalt {  get; set; }
    }
}
