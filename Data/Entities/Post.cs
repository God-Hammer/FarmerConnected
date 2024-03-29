﻿namespace Data.Entities
{
    public partial class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid MarketPriceId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual MarketPrice MarketPrice { get; set; } = null!;
    }
}
