using CartService.Model;
using CartService.Model.Dtos;

namespace CartService.Services.IServices
{
    public interface ICart
    {
        Task<string> AddToCart(Cart cart);
        Task<string> RemoveFromCart(Cart cart);
        Task<List<Cart>> GetCartItems(Guid userId);
        Task<Cart> GetCartItem(Guid id);
        Task saveChanges();
    }
}
