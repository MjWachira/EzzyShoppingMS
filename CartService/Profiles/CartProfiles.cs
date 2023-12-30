using AutoMapper;
using CartService.Model;
using CartService.Model.Dtos;

namespace CartService.Profiles
{
    public class CartProfiles:Profile
    {
        public CartProfiles()
        {
            CreateMap<AddToCartDto, Cart>().ReverseMap();
        }
    }
}
