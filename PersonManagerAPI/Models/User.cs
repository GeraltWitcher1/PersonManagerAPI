using System.ComponentModel.DataAnnotations;

namespace PersonManagerAPI.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}
