using TMDT.Models;

namespace TMDT.Services
{
	public class OrderItemService
	{
		db_ECommerceShopContext _context;

		public OrderItemService()
		{
			_context = new db_ECommerceShopContext();
		}

		public List<OrderItem> GetAllOrderItems()
		{
			return _context.OrderItems.ToList();
		}

		public OrderItem GetOrderItemByProductId(int productId)
		{
			return _context.OrderItems.FirstOrDefault(c => c.ProductId == productId);
		}

		public List<OrderItem> GetCartOrderItem(int OrderId)
		{
			return _context.OrderItems.Where(c => c.OrderId == OrderId).ToList();
		}

		public bool CheckCart(int idProduct)
		{
			return _context.OrderItems.Any(c => c.ProductId == idProduct) ? true : false;
		}

		public bool AddOrderItem(OrderItem orderItem)
		{
			try
			{
				_context.OrderItems.Add(orderItem);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateOrderItem(OrderItem orderItem)
		{
			try
			{
				var oi = GetOrderItemByProductId(orderItem.ProductId);
				oi.Quantity = orderItem.Quantity;
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteOrderItem(int productId)
		{
			try
			{
				var oi = GetOrderItemByProductId(productId);
				_context.OrderItems.Remove(oi);
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
