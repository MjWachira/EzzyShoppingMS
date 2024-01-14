using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Services.IServices;
using OrderService.Models.Dtos;
using OrderService.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Azure;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICart _cartService;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly IOrder _orderService;
        public OrderController(ICart cart, IMapper   mapper,
          IOrder order)
        {
            _cartService = cart;
            _mapper = mapper;
            _orderService = order;
            _responseDto = new ResponseDto();   
        }
        
        [HttpPost("{Id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> MakeOrder(MakeOrderDto orderDto, Guid Id)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login";
                return Unauthorized(_responseDto);
            }
            var cartItem = await _cartService.GetCartItemById(Id);
            if (cartItem == null )
            {
                _responseDto.Errormessage = "Items Not Found";
                return NotFound(_responseDto);
            }
            var order = _mapper.Map<Orders>(orderDto);
            order.CartItemsId = cartItem.Id;
            order.UserId = Guid.Parse(UserId);
            order.ProductTotal = cartItem.ProductTotal;

            var res = await _orderService.MakeOrder(order);
            _responseDto.Result = res;
            return Ok(_responseDto);

        }

       

        [HttpPost("Pay")]
        public async Task<ActionResult<ResponseDto>> MakePayments(StripeRequestDto dto)
        {

            var sR = await _orderService.MakePayments(dto);
            _responseDto.Result = sR;
            return Ok(_responseDto);
        }
        /*

        [HttpPost("validate/{Id}")]
        public async Task<ActionResult<ResponseDto>> validatePayment(Guid Id)
        {

            var res = await _orderService.ValidatePayments(Id);

            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }

            _responseDto.Errormessage = "Payment Failed!";
            return BadRequest(_responseDto);
        }
       */
    }

}
