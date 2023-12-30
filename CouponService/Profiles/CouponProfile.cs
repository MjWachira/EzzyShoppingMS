using AutoMapper;
using CouponService.Models;
using CouponService.Models.Dtos;

namespace CouponService.Profiles
{
    public class CouponProfile:Profile
    {
        public CouponProfile()
        {
            CreateMap<AddCouponDto, Coupon>().ReverseMap();
        }
    }
}
