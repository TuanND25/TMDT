using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TMDT.Models;
using TMDT.Services;

namespace TMDT.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ProductService _productService;
		private readonly CategoryService _categoryService;
		private readonly OrderItemService _orderItemService;
		private readonly OrderService _orderService;
		public static string keySearch { get; set; }
		public static string keyFilter { get; set; }
		public static int keyDetailP { get; set; }
		const int _statusCart = -1;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			_productService = new ProductService();
			_categoryService = new CategoryService();
			_orderItemService = new OrderItemService();
			_orderService = new OrderService();
		}

		public IActionResult Index()
		{
			var lstprd = _productService.GetAllProducts().Take(8);
			return View(lstprd);
		}

		[Route("all-product")]
		public IActionResult ShowAllProduct()
		{
			var lstprd = _productService.GetAllProducts();
			return View(lstprd);
		}

		[Route("detail-product/detail-product")]
		public IActionResult DetailProduct(int productId)
		{
			if (productId != null || productId != 0)
			{
				if (productId != keyDetailP)
				{
					// Giữ nguyên giá trị của tham số query nếu nó tồn tại
					keyDetailP = productId;
					return RedirectToAction("DetailProduct", new { productId = productId });
				}
			}
			var productDetail = _productService.GetProductByProductId(productId);
			return View(productDetail);
		}

		[Route("search-product")]
		public IActionResult SearchProduct(string? search)
		{
			if (!string.IsNullOrEmpty(search))
			{
				if (search != keySearch)
				{
					// Giữ nguyên giá trị của tham số query nếu nó tồn tại
					keySearch = search;
					return RedirectToAction("SearchProduct", new { search = search });
				}
			}
			var lstSearch = _productService.GetAllProducts().Where(c => c.Name.Trim().ToLower().Contains(search.Trim().ToLower())).ToList() ?? new();
			ViewBag.keySearch = search;
			return View(lstSearch);
		}

		[Route("all-product/category")]
		public IActionResult FilterCate(string? filter)
		{
			if (!string.IsNullOrEmpty(filter))
			{
				if (filter != keyFilter)
				{
					// Giữ nguyên giá trị của tham số query nếu nó tồn tại
					keyFilter = filter;
					return RedirectToAction("FilterCate", new { filter = filter });
				}
			}
			var cateid = _categoryService.GetCategoryByCategoryName(filter ?? default).CategoryId;
			var lstSearch = _productService.GetAllProducts().Where(c => c.CategoryId == cateid).ToList() ?? new();
			ViewBag.keySearch = filter;
			return View(lstSearch);
		}

		[Route("/cart")]
		[HttpGet]
		public IActionResult ShowCart()
		{
			List<OrderItem> lst = new();
			var userId = HttpContext.Session.GetString("Id");
			if (string.IsNullOrEmpty(userId)) lst = new();
			else
			{
				var orderCart = _orderService.GetOrderCart(int.Parse(userId)) ?? new();
				lst = _orderItemService.GetCartOrderItem(orderCart.OrderId) ?? new();
			}
			return View(lst);
		}
		public IActionResult AddToCart(int idProduct, int quantity)
		{
			var userId = HttpContext.Session.GetString("Id");
			if (string.IsNullOrEmpty(userId)) return Content("Vui lòng đăng nhập");
			else
			{
				var orderCart = _orderService.GetOrderCart(int.Parse(userId));
				if (orderCart != null)
				{
					if (_orderItemService.CheckCart(idProduct))
					{
						var updateP = _orderItemService.GetOrderItemByProductId(idProduct);
						_orderItemService.UpdateOrderItem(new()
						{
							OrderId = orderCart.OrderId,
							ProductId = idProduct,
							Quantity = updateP.Quantity + quantity,
							Price = _productService.GetProductByProductId(idProduct).LastPrice
						});
						return RedirectToAction("ShowCart");
					}
					else
					{
						OrderItem odi = new()
						{
							OrderId = orderCart.OrderId,
							ProductId = idProduct,
							Quantity = quantity,
							Price = _productService.GetProductByProductId(idProduct).LastPrice
						};
						_orderItemService.AddOrderItem(odi);
						return RedirectToAction("ShowCart");
					}
				}
				else
				{
					Order od = new()
					{
						UserId = int.Parse(userId),
						CreatedAt = DateTime.Now,
						Status = _statusCart
					};
					_orderService.AddOrder(od);
					_orderItemService.AddOrderItem(new()
					{
						OrderId = od.OrderId,
						ProductId = idProduct,
						Quantity = quantity,
						Price = _productService.GetProductByProductId(idProduct).LastPrice
					});
					return RedirectToAction("ShowCart");
				}
			}
		}

		[HttpGet]
		public IActionResult DeleteOI(int ProductId)
		{
			_orderItemService.DeleteOrderItem(ProductId);
			return RedirectToAction("ShowCart");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}