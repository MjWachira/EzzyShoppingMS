using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;
using ProductService.Models.Dtos;
using ProductService.Services.IServices;

namespace ProductService.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductServices( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddNewProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return "Product Added Successfully";
        }

        public async Task<List<ProductAndImagesResponseDto>> GetAllProducts()
        {
            return await _context.Product.Select(p =>  new ProductAndImagesResponseDto()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                ProductImages = p.ProductImages.Select(x => new AddProductImageDto()
                {
                  Image = x.Image,
                }).ToList(),
            } ).ToListAsync();
        }

        public async Task<Product> GetProduct(Guid Id)
        {
            return await _context.Product.Where(b => b.Id == Id).FirstOrDefaultAsync();
        } 
    }
}
