using Newtonsoft.Json;
using System.Text;
using zEzzyShoppingFrontend.Models;
using zEzzyShoppingFrontend.Models.Cart;

namespace zEzzyShoppingFrontend.Services.Cart
{
    public class CartServices : ICart
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://localhost:7777";
        public CartServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task <ResponseDto> AddToCart(CartDto cartDto)
        {
            var request = JsonConvert.SerializeObject(cartDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Cart", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }
            return new ResponseDto();
        }

        Task<ResponseDto> ICart.ApplyCoupons(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICart.CartDelete(Guid userId)
        {
            throw new NotImplementedException();
        }

        Task<CartDto> ICart.GetCartByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto> ICart.RemoveFromCart(Guid cartDetailId)
        {
            throw new NotImplementedException();
        }
    }
}
