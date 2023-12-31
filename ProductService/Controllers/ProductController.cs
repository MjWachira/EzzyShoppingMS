using AutoMapper;
using Azure;
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
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;
        public ProductController(IProduct product,
            IMapper mapper)
        {
            _productService = product;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> AddProduct(AddProductDto product)
        {
            var prod = _mapper.Map<Product>(product);
            var res = await _productService.AddNewProduct(prod);
            _responseDto.Result = res;
            return Created("", _responseDto);

        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> getProducts()
        {
            var res = await _productService.GetAllProducts();
            _responseDto.Result = res;
            return Ok(_responseDto);

        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetCoupon(Guid Id)
        {
            var product = await _productService.GetProduct(Id);

            if (product == null)
            {
                _responseDto.Errormessage = "Product Not found";
                return NotFound(_responseDto);
            }
            _responseDto.Result = product;
            return Ok(_responseDto);

        }
    }
}
