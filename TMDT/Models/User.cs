using System;
using System.Collections.Generic;

namespace TMDT.Models
{
    public partial class User
    {
        public User()
        {
            OrderHistories = new HashSet<OrderHistory>();
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserRole { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool? Gender { get; set; }
        public string? AddressInfo { get; set; }
        public string? PhoneNum { get; set; }

        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
