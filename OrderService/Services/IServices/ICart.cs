using OrderService.Models.Dtos;

namespace OrderService.Services.IServices
{
    public interface ICart
    {
        Task<CartDto> GetCartItemById(Guid id);
    }
}
