namespace Data.Models.Views
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }
        public CategoryViewModel Category { get; set; } = null!;

    }
}
