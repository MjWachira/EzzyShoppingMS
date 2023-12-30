using AutoMapper;
using ProductService.Models;
using ProductService.Models.Dtos;

namespace ProductService.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles()
        {
            CreateMap<AddProductDto, Product>().ReverseMap();
            CreateMap<AddProductImageDto, ProductImage>().ReverseMap();
        }
    }
}
