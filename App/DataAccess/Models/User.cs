using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
