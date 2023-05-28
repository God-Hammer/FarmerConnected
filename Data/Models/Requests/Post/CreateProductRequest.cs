namespace Data.Models.Requests.Post
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }

    }
}
