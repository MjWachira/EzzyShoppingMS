using AutoMapper;
using CartService.Model;
using CartService.Model.Dtos;
using CartService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cartservice;
        private readonly IProduct _productservice;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public CartController(ICart cart, IProduct product, IMapper mapper)
        {
            _cartservice = cart;
            _productservice = product;
            _mapper = mapper;
            _response = new ResponseDto();
        }
        [HttpPost("{Id}")]
        public async Task<ActionResult<ResponseDto>> AddToCart(AddToCartDto newCart, Guid Id)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to add product to cart";
            }
            var product = await _productservice.GetProductById(Id);
            if (string.IsNullOrWhiteSpace(product.ProductDescription))
            {
                _response.Errormessage = "Product Not Found";
                return NotFound(_response);
            }
            var cartItem = _mapper.Map<Cart>(newCart);
            cartItem.UserId = Guid.Parse(UserId);
            cartItem.ProductId = Id;
            cartItem.ProductPrice = product.Price;
            cartItem.ProductTotal = product.Price *cartItem.ProductCount;    

            var res = await _cartservice.AddToCart(cartItem);
            _response.Result = res;
            return Ok(_response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>>MyCartItems()
        {
            var UserId =  User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null) 
            {
                _response.Errormessage = "Please login to view cart";
            }

            var cartItems= await _cartservice.GetCartItems(Guid.Parse(UserId));
            
            _response.Result = cartItems;
            return Ok(_response);
                       
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseDto>> RemoveFromCart(Guid Id)
        {
            var CartItem = await _cartservice.GetCartItem(Id);
            if (CartItem == null)
            {
                _response.Result = "Coupon Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            var res = await _cartservice.RemoveFromCart(CartItem);
            _response.Result = res;
            return Ok(_response);
        }

    }

}
