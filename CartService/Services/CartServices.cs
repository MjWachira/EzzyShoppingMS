using CartService.Data;
using CartService.Model;
using CartService.Model.Dtos;
using CartService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CartService.Services
{
    public class CartServices : ICart
    {
        private readonly ApplicationDbContext _context;
        public CartServices(ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<string> AddToCart(Cart cart)
        {
            _context.Cart.Add( cart );
            await _context.SaveChangesAsync();
            return "Product Added To Cart";
        }

        public async Task<List<Cart>> GetCartItems(Guid userId)
        {
            return await _context.Cart.Where(b => b.UserId == userId).ToListAsync();
        }

        public Task<string> RemoveFromCart(Guid userId, Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
