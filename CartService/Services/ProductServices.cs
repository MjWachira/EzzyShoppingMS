using CartService.Model.Dtos;
using CartService.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http;

namespace CartService.Services
{
    public class ProductServices : IProduct
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ProductDto> GetProductById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"{Id}");
            ////string////
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
            }
            return new ProductDto();
        }

    }
}
