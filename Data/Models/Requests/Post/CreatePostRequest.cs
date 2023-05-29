namespace Data.Models.Requests.Post
{
    public class CreatePostRequest
    {
        public string Title { get; set; } = null!;
        public Guid MarketPriceId { get; set; }
        public string Description { get; set; } = null!;
    }
}
