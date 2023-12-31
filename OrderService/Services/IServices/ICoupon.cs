using OrderService.Models.Dtos;

namespace OrderService.Services.IServices
{
    public interface ICoupon
    {
        Task<CouponDto> GetCouponByCouponCode(string couponCode);
    }
}
