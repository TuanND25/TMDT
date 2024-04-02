using System;
using System.Collections.Generic;

namespace TMDT.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderHistories = new HashSet<OrderHistory>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
