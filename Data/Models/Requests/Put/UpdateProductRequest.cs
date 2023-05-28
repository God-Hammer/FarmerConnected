namespace Data.Models.Requests.Put
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
    }
}
