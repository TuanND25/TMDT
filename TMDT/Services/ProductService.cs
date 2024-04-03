using TMDT.Models;

namespace TMDT.Services
{
    public class ProductService
    {
        db_ECommerceShopContext _context;

        public ProductService()
        {
            _context = new db_ECommerceShopContext();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductByProductId(int productId)
        {
            return _context.Products.FirstOrDefault(c => c.ProductId == productId);
        }

        public bool AddProduct(Product product)
        {
            try
            {
                //product.ProductId = _context.Products.Count() == 0 ? 1 : _context.Products.Max(c => c.ProductId) + 1;
                product.ProductId = null;
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                var productUpdate = GetProductByProductId(product.ProductId ?? default);
                productUpdate.Name = product.Name;
                productUpdate.Description = product.Description;
                productUpdate.Price = product.Price;
                productUpdate.LastPrice = product.LastPrice;
                productUpdate.CategoryId = product.CategoryId;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                var product = GetProductByProductId(productId);
                _context.Products.Remove(product);
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
