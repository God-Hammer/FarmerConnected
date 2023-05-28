using Data.Entities;

namespace Data.Models.Views
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }
        public MarketPriceViewModel MarketPrice { get; set; } = null!;
    }
}
