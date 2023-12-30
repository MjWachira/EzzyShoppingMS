using CartService.Model.Dtos;
using CartService.Services.IServices;

namespace CartService.Services
{
    public class ProductServices : IProduct
    {
        public Task<ProductDto> GetProductById(Guid ProductId)
        {
            throw new NotImplementedException();
        }
    }
}
