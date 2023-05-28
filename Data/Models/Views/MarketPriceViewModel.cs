namespace Data.Models.Views
{
    public class MarketPriceViewModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
