using ProductService.Data;
using ProductService.Models;
using ProductService.Services.IServices;

namespace ProductService.Services
{
    public class ImageServices : IImage
    {
        private readonly ApplicationDbContext _context;
        public ImageServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddImage(Guid Id, ProductImage image)
        {
            var product = _context.Product.Where(b => b.Id == Id).FirstOrDefault();
            product.ProductImages.Add(image);
            await _context.SaveChangesAsync();
            return "Image added successfully";
        }
    }
}
