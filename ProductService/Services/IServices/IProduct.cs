using ProductService.Models;
using ProductService.Models.Dtos;

namespace ProductService.Services.IServices
{
    public interface IProduct
    {
        Task<List<ProductAndImagesResponseDto>> GetAllProducts();
        Task<Product> GetProduct(Guid Id);
        Task<string> AddNewProduct(Product product);
    }
}
