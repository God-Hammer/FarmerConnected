using System.ComponentModel.DataAnnotations;

namespace Data.Models.Requests.Post
{
    public class RegisterRequest
    {

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(6)]
        public string Password { get; set; } = null!;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
