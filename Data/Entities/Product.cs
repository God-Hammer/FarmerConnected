using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CategoryId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
