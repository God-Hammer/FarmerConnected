namespace Data.Entities
{
    public partial class MarketPrice
    {
        public MarketPrice()
        {
            Posts = new HashSet<Post>();
        }

        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
