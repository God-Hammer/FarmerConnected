using Utility.Constant;

namespace Data.Models.Views
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }

        public UserStatus? IsActive { get; set; }

        public DateTime CreateAt { get; set; }

    }
}
