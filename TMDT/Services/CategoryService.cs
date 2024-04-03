using TMDT.Models;

namespace TMDT.Services
{
	public class CategoryService
	{
		db_ECommerceShopContext _context;

		public CategoryService()
		{
			_context = new db_ECommerceShopContext();
		}
		public List<Category> GetAllCategories()
		{
			return _context.Categories.ToList();
		}

		public Category GetCategoryByCategoryId(int categoryId)
		{
			return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
		}

		public Category GetCategoryByCategoryName(string categoryName)
		{
			return _context.Categories.FirstOrDefault(c => c.Name == categoryName);
		}

		public bool AddCategory(Category categorie)
		{
			try
			{
				//categorie.CategoryId = _context.Categories.Count() == 0 ? 1 : _context.Categories.Max(c => c.CategoryId) + 1;
				categorie.CategoryId = null;
				_context.Categories.Add(categorie);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool UpdateCategory(Category category)
		{
			try
			{
				var categorieUpdate = GetCategoryByCategoryId(category.CategoryId ?? default);
				categorieUpdate.Name = category.Name;
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool DeleteCategory(int categoryId)
		{
			try
			{
				var Categorie = GetCategoryByCategoryId(categoryId);
				_context.Categories.Remove(Categorie);
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
