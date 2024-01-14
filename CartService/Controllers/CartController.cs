using AutoMapper;
using CartService.Model;
using CartService.Model.Dtos;
using CartService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICoupon _couponService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public CartController(ICart cart, IProduct product, IMapper mapper , ICoupon coupon)
        {
            _cartservice = cart;
            _couponService = coupon;
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
                _response.Errormessage = "Please login to add a product to the cart";
                return Unauthorized(_response);
            }

            try
            {
                var product = await _productservice.GetProductById(Id);
                if (string.IsNullOrWhiteSpace(product.ProductDescription))
                {
                    _response.Errormessage = "Product Not Found";
                    return NotFound(_response);
                }
                var cartItem = _mapper.Map<Cart>(newCart);
                cartItem.UserId = Guid.Parse(UserId);
                cartItem.ProductCount = 1;
                cartItem.ProductId = Id;
                cartItem.ProductPrice = product.Price;
                cartItem.ProductTotal = product.Price * cartItem.ProductCount;

                var res = await _cartservice.AddToCart(cartItem);
                _response.Result = res;
                return Ok(_response);


            }
            catch (Exception ex)
            {
                _response.Errormessage = "An error occurred while processing the request.";
                return StatusCode(500, _response);
            }
        }



        [HttpGet]
        public async Task<ActionResult<ResponseDto>>MyCartItems()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to view your cart";
                return Unauthorized(_response);
            }

            var cartItems= await _cartservice.GetCartItems(Guid.Parse(UserId));
            
            _response.Result = cartItems;
            return Ok(_response);
                       
        }
        [HttpGet("single/{Id}")]
        public async Task<ActionResult<ResponseDto>> GetCoupon(Guid Id)
        {
            var CartItem = await _cartservice.GetCartItem(Id);
            if (CartItem == null)
            {
                _response.Result = "Item Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = CartItem;
            return Ok(_response);
        }

        [HttpPut("updatecart/{Id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> updateCart(Guid Id, AddToCartDto uItem)
        {
            var cItem = await _cartservice.GetCartItem(Id);
            if (cItem == null)
            {
                _response.Result = "Not Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            var cartItem = _mapper.Map<Cart>(uItem);
            var res = await _cartservice.UpdateCart(cartItem);
            _response.Result = res;
            return Ok(_response);

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ResponseDto>> ApplyCoupon(Guid Id, string Code)
        {

            var cartItem = await _cartservice.GetCartItem(Id);

            if (cartItem == null)
            {
                _response.Errormessage = "order Not Found";
                return NotFound(_response);
            }
            var coupon = await _couponService.GetCouponByCouponCode(Code);
            if (coupon == null)
            {
                _response.Errormessage = "Coupon is not Valid";
                return NotFound(_response);
            }

            if (coupon.CouponMinAmount <= cartItem.ProductTotal)
            {
                cartItem.CouponCode = coupon.CouponCode;
                cartItem.Discount = coupon.CouponAmount;
                cartItem.ProductTotal = cartItem.ProductTotal - coupon.CouponAmount;
                await _cartservice.saveChanges();
                _response.Result = "Code applied";
                return Ok(_response);
            }
            else
            {
                _response.Errormessage = "Total amount is less that the minimum amount for this coupon";
                return BadRequest(_response);
            }
            

        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseDto>> RemoveFromCart(Guid Id)
        {
            var CartItem = await _cartservice.GetCartItem(Id);
            if (CartItem == null)
            {
                _response.Result = "Item Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            var res = await _cartservice.RemoveFromCart(CartItem);
            _response.Result = res;
            return Ok(_response);
        }

    }

}
