using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;
using Stripe;
using Stripe.Checkout;

namespace OrderService.Services
{
    public class OrderServices : IOrder
    {
        private readonly ApplicationDbContext _context;
        public OrderServices(ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<List<Orders>> GetAllOrders(Guid userId)
        {
            return await _context.Orders.Where(b=>b.UserId==userId).ToListAsync();
        }

        public async Task<Orders> GetOrderById(Guid Id)
        {
            return await _context.Orders.Where(b => b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> MakeOrder(Orders order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return "Order Placed Successfully";
        }

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var order = await _context.Orders.Where(x => x.Id == stripeRequestDto.OrderId).FirstOrDefaultAsync();
           
            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };



            var item = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)order.ProductTotal * 100,
                    Currency = "kes",

                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        
                    }
                },
                Quantity = 1


            };

            options.LineItems.Add(item);

            //discount

            var DiscountObj = new List<SessionDiscountOptions>()
            {
                new SessionDiscountOptions()
                {
                    Coupon=order.CouponCode
                }
            };

            if (order.Discount > 0)
            {
                options.Discounts = DiscountObj;

            }
            var service = new SessionService();
            Session session = service.Create(options);

            // URL//ID

            stripeRequestDto.StripeSessionUrl = session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            //update Database =>status/ SessionId 

            order.StripeSessionId = session.Id;
            order.Status = "Ongoing";
            await _context.SaveChangesAsync();

            return stripeRequestDto;
        }

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidatePayments(Guid orderId)
        {
            var order = await _context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();

            var service = new SessionService();
            Session session = service.Get(order.StripeSessionId);

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
           
                order.Status = "Paid";
                order.PaymentIntent = paymentIntent.Id;
                await _context.SaveChangesAsync();

                return true;

            }
            return false;
        }
    }
    
}
