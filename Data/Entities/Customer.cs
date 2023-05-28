namespace Data.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public bool? IsActive { get; set; }
        public string VerifyToken { get; set; } = null!;
        public DateTime? VerifyTime { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
