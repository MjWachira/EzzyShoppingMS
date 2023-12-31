using OrderService.Models;
using OrderService.Models.Dtos;

namespace OrderService.Services.IServices
{
    public interface IOrder
    {
        Task<string> MakeOrder(Orders order);
        Task<List<Orders>> GetAllOrders(Guid userId);
        Task<Orders> GetOrderById(Guid Id);
        Task saveChanges();

        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);
        Task<bool> ValidatePayments(Guid orderId);
    }
}
