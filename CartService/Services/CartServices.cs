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
        public async Task<Cart> GetCartItem(Guid Id)
        {
            return await _context.Cart.Where(b => b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> RemoveFromCart(Cart cart)
        {
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return "Item Removed from Cart";

        }
    }
}
