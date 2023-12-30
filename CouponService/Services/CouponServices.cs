using CouponService.Data;
using CouponService.Models;
using CouponService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CouponService.Services
{
    public class CouponServices : ICoupon
    {
        private readonly ApplicationDbContext _context;
        public CouponServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddCoupon(Coupon coupon)
        {
            _context.Coupon.Add(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Added!!";
        }

        public async Task<string> DeleteCoupon(Coupon coupon)
        {
            _context.Coupon.Remove(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Removed!!";
        }

        public async Task<List<Coupon>> GetAllCoupons()
        {
            return await _context.Coupon.ToListAsync();
        }

        public async Task<Coupon> GetCoupon(Guid Id)
        {
            return await _context.Coupon.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Coupon> GetCoupon(string code)
        {
            return await _context.Coupon.Where(x => x.CouponCode == code).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateCoupon()
        {
            await _context.SaveChangesAsync();
            return "Coupon Updated !!";
        }
    }
}
