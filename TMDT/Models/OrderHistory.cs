using System;
using System.Collections.Generic;

namespace TMDT.Models
{
    public partial class OrderHistory
    {
        public int OrderHistoryId { get; set; }
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public int UserId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Note { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;
        public virtual OrderStatus OrderStatus { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
