using ProductService.Models;

namespace ProductService.Services.IServices
{
    public interface IImage
    {
        Task<string> AddImage(Guid Id, ProductImage image);
    }
}
