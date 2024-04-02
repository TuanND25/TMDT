using System;
using System.Collections.Generic;

namespace TMDT.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            OrderHistories = new HashSet<OrderHistory>();
        }

        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; } = null!;

        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
    }
}
