using CartService.Model.Dtos;
using CartService.Services.IServices;
using Newtonsoft.Json;

namespace CartService.Services
{
    public class CouponService : ICoupon
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
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
