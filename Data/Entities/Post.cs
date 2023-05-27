using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Post
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public Guid MarketPriceId { get; set; }
        public int Price { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual MarketPrice MarketPrice { get; set; } = null!;
    }
}
