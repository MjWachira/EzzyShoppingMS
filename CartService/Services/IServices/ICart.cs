using CartService.Model;
using CartService.Model.Dtos;

namespace CartService.Services.IServices
{
    public interface ICart
    {
        Task<string> AddToCart(Cart cart);
        Task<string> RemoveFromCart(Guid userId, Guid productId);
        Task<List<Cart>> GetCartItems(Guid userId);
    }
}
