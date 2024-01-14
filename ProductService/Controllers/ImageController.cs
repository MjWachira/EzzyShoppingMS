using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Models.Dtos;
using ProductService.Services.IServices;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImage _imageService;
        private readonly IProduct _productService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        public ImageController(IImage image, IProduct product,
            IMapper mapper)
        {
            _imageService=image;
            _productService=product;
            _mapper=mapper;
            _responseDto =new ResponseDto();
        }
        [HttpPost("{Id}")]
        public async Task<ActionResult<ResponseDto>> AddImage(Guid Id, AddProductImageDto image)
        {
            var product = await _productService.GetProduct(Id);

            if (product == null)
            {
                _responseDto.Errormessage = "Product Not Found";
                return NotFound(_responseDto);
            }

            var img = _mapper.Map<ProductImage>(image);
            var res = await _imageService.AddImage(Id, img);
            _responseDto.Result = res;
            return Created("", _responseDto);

        }

    }
}
