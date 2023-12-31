using Newtonsoft.Json;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;

namespace OrderService.Services
{
    public class CartServices : ICart
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CartServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CartDto> GetCartItemById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Cart");
            var response = await client.GetAsync(Id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CartDto>(responseDto.Result.ToString());
            }
            return new CartDto();
        }
    }
}
