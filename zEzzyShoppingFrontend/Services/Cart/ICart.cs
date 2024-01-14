using zEzzyShoppingFrontend.Models;
using zEzzyShoppingFrontend.Models.Cart;


namespace zEzzyShoppingFrontend.Services.Cart
{
    public interface ICart
    {
        Task<ResponseDto> AddToCart(CartDto cartDto);

        Task<CartDto> GetCartByUserId(Guid userId);

        Task<ResponseDto> ApplyCoupons(CartDto cartDto);


        Task<ResponseDto> RemoveFromCart(Guid cartDetailId);

        Task<bool> CartDelete(Guid userId);

    }
}
