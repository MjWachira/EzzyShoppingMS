using CartService.Model.Dtos;

namespace CartService.Services.IServices
{
    public interface IProduct
    {
        Task<ProductDto> GetProductById(Guid Id);

    }
}
