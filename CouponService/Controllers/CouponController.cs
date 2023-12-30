﻿using AutoMapper;
using CouponService.Models;
using CouponService.Models.Dtos;
using CouponService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICoupon _couponService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public CouponController(ICoupon coupon, IMapper mapper)
        {
            _couponService = coupon;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCoupons();
            _response.Result = coupons;
            return Ok(_response);
        }

        [HttpGet("single/{Id}")]
        public async Task<ActionResult<ResponseDto>> GetCoupon(Guid Id)
        {
            var coupon = await _couponService.GetCoupon(Id);
            if (coupon == null)
            {
                _response.Errormessage = "Coupon Not found";
                return NotFound(_response);
            }
            _response.Result = coupon;
            return Ok(_response);
        }

        [HttpGet("{Code}")]
        public async Task<ActionResult<ResponseDto>> GetCoupon(string Code)
        {
            var coupon = await _couponService.GetCoupon(Code);
            if (coupon == null)
            {
                _response.Errormessage = "Coupon Not found";
                return NotFound(_response);
            }
            _response.Result = coupon;
            return Ok(_response);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddCoupon(AddCouponDto newCoupon)
        {
            var coupon = _mapper.Map<Coupon>(newCoupon);
            var response = await _couponService.AddCoupon(coupon);
            _response.Result = response;
            return Created("", _response);
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> updateCoupon(Guid Id, AddCouponDto UCoupon)
        {
            var coupon = await _couponService.GetCoupon(Id);
            if (coupon == null)
            {
                _response.Result = "Coupon Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _mapper.Map(UCoupon, coupon);
            var res = await _couponService.UpdateCoupon();
            _response.Result = res;
            return Ok(_response);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> deleteCoupon(Guid Id)
        {
            var coupon = await _couponService.GetCoupon(Id);
            if (coupon == null)
            {
                _response.Result = "Coupon Not Found";
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            var res = await _couponService.DeleteCoupon(coupon);
            _response.Result = res;
            return Ok(_response);
        }
    }
}
