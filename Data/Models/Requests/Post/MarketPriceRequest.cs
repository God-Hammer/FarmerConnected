namespace Data.Models.Requests.Post
{
    public class MarketPriceRequest
    {
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
    }
}
