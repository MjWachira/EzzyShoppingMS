using CartService.Model.Dtos;

namespace CartService.Services.IServices
{
    public interface ICoupon
    {
        Task<CouponDto> GetCouponByCouponCode(string couponCode);
    }
}
