using TMDT.Models;

namespace TMDT.Services
{
	public class OrderService
	{
		db_ECommerceShopContext _context;
		const int _statusCart = -1;
		public OrderService()
		{
			_context = new db_ECommerceShopContext();
		}

		public List<Order> GetAllOrders()
		{
			return _context.Orders.ToList();
		}

		public Order GetOrderByOrderId(int OrderId)
		{
			return _context.Orders.FirstOrDefault(c => c.OrderId == OrderId);
		}

		public Order GetOrderCart(int userId)
		{
			return _context.Orders.FirstOrDefault(c => c.UserId == userId && _statusCart == -1);
		}

		public bool AddOrder(Order Order)
		{
			try
			{
				Order.OrderId = _context.Orders.Count() == 0 ? 1 : _context.Orders.Max(c => c.OrderId) + 1;
				//Order.OrderId = null;
				_context.Orders.Add(Order);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateOrder(Order Order)
		{
			try
			{
				var OrderUpdate = GetOrderByOrderId(Order.OrderId);
				OrderUpdate.CreatedAt = Order.CreatedAt;
				OrderUpdate.UpdatedAt = Order.UpdatedAt;
				OrderUpdate.Status = Order.Status;
				OrderUpdate.ShippingCost = Order.ShippingCost;
				OrderUpdate.Tax = Order.Tax;
				OrderUpdate.Discount = Order.Discount;
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteOrder(int OrderId)
		{
			try
			{
				var Order = GetOrderByOrderId(OrderId);
				_context.Orders.Remove(Order);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
