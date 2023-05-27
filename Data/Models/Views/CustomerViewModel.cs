using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constraints;

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
