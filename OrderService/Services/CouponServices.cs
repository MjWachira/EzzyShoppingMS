using Newtonsoft.Json;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;

namespace OrderService.Services
{
    
    public class CouponServices : ICoupon
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCouponByCouponCode(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync(couponCode);
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CouponDto>(responseDto.Result.ToString());
            }
            return new CouponDto();
        }
    }
}
