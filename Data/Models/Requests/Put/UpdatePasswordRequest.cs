using System.ComponentModel.DataAnnotations;

namespace Data.Models.Requests.Put
{
    public class UpdatePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Please inputat least 6 characters"), MinLength(6)]
        public string NewPassword { get; set; } = null!;

        [Required, Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
