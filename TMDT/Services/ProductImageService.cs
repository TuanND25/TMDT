using TMDT.Models;

namespace TMDT.Services
{
    public class ProductImageService
    {
        db_ECommerceShopContext _context;

        public ProductImageService()
        {
            _context = new db_ECommerceShopContext();
        }

        public List<ProductImage> GetAllProductImages()
        {
            return _context.ProductImages.ToList();
        }

        public ProductImage GetProductImageByProductImageId(int ProductImageId)
        {
            return _context.ProductImages.FirstOrDefault(c => c.Id == ProductImageId);
        }

		public List<ProductImage> GetProductImageByProductId(int ProductId)
		{
			return _context.ProductImages.Where(c => c.ProductId == ProductId).OrderBy(c => c.Id).ToList();
		}

		public bool AddProductImage(ProductImage ProductImage)
        {
            try
            {
                ProductImage.Id = _context.ProductImages.Count() == 0 ? 1 : _context.ProductImages.Max(c => c.Id) + 1;
                _context.ProductImages.Add(ProductImage);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProductImage(ProductImage productImage)
        {
            try
            {
                var ProductImageUpdate = GetProductImageByProductImageId(productImage.Id ?? default);
                if (ProductImageUpdate != null)
                {
                    ProductImageUpdate.Url = productImage.Url;
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProductImage(int ProductImageId)
        {
            try
            {
                var ProductImage = GetProductImageByProductImageId(ProductImageId);
                _context.ProductImages.Remove(ProductImage);
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
