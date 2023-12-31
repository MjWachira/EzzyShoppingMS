using Microsoft.EntityFrameworkCore;
using OrderService.Extensions;
using OrderServiceExtensions;
using OrderService.Data;
using OrderService.Services.IServices;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddAuth();
builder.AddSwaggenGenExtension();

builder.Services.AddHttpClient("Coupon", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:CouponService")));
builder.Services.AddHttpClient("user", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:AuthService")));
builder.Services.AddHttpClient("Cart", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:CartService")));

// FOR DI
builder.Services.AddScoped<ICart, CartServices>();
builder.Services.AddScoped<ICoupon, CouponServices>();
builder.Services.AddScoped<IOrder, OrderServices>();
builder.Services.AddScoped<IUser, UserServices>();

var app = builder.Build();

Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:Key");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMigrations();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
