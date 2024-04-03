using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TMDT.Models;
using TMDT.Services;

namespace TMDT.Controllers
{
	public class ProductController : Controller
	{
		private ProductService _productService;
		private ProductImageService _productImageService;
		private CategoryService _categoryService;
		private List<string> _lstCateText;
		public static List<IFormFile> _lstImg;
		public ProductController()
		{
			_productService = new();
			_productImageService = new();
			_categoryService = new();
			_lstCateText = new();
			_lstImg = new();
			var lstcate = _categoryService.GetAllCategories();
			foreach (var item in lstcate)
			{
				_lstCateText.Add(item.Name);
			}
		}
		[Route("product-management")]
		public IActionResult Adm_ProductList()
		{
			var lst = _productService.GetAllProducts();
			return View(lst);
		}

		[Route("product-management/create")]
		public IActionResult Adm_CreateProduct()
		{
			ViewBag.LstCate = _lstCateText;
			return View();
		}

		[HttpPost]
		[Route("product-management/create")]
		public IActionResult Adm_CreateProduct(Product prd, string Price, string LastPrice)
		{
			prd.Price = decimal.Parse(Price, CultureInfo.InvariantCulture);
			prd.LastPrice = decimal.Parse(LastPrice, CultureInfo.InvariantCulture);
			if (_productService.AddProduct(prd))
			{
				if (_lstImg.Count() > 0)
				{
					foreach (var item in _lstImg.Distinct())
					{
						if (item != null && item.Length > 0)
						{
							// Trỏ tới thư mục wwwroot để lát nữa thực hiện việc copy sang
							var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", item.FileName);
							using (var stream = new FileStream(path, FileMode.Create))
							{
								// Thực hiện copy ảnh vừa chọn sang thư mục mới (wwwroot)
								item.CopyTo(stream);
							}
							// Add ảnh vào db
							_productImageService.AddProductImage(new ProductImage
							{
								ProductId = prd.ProductId ?? default,
								Url = item.FileName
							});
						}
					}
				}
				return RedirectToAction("Adm_ProductList");
			}
			else return Content("Thêm không thành công");
		}

		[HttpGet]
		[Route("product-management/edit/{id}")]
		public IActionResult Adm_UpdateProduct(int id)
		{
			ViewBag.LstCate = _lstCateText;
			var prd = _productService.GetProductByProductId(id);
			return View(prd);
		}

		[Route("product-management/edit/{id}")]
		public IActionResult Adm_UpdateProduct(Product prd, string Price, string LastPrice)
		{
			prd.Price = decimal.Parse(Price, CultureInfo.InvariantCulture);
			prd.LastPrice = decimal.Parse(LastPrice, CultureInfo.InvariantCulture);
			if (_productService.UpdateProduct(prd))
			{
				return RedirectToAction("Adm_ProductList");
			}
			else return Content("Cập nhật không thành công");
		}

		public IActionResult Adm_DeleteProduct(int id)
		{
			if (_productService.DeleteProduct(id))
			{
				return RedirectToAction("Adm_ProductList");
			}
			else return Content("Xóa không thành công");
		}
	}
}