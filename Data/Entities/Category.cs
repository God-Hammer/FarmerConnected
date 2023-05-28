namespace Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
