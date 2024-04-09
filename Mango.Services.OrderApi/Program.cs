using AutoMapper;
using Mango.Azure.ServiceBus;
using Mango.Services.OrderApi;
using Mango.Services.OrderApi.AutoMapper;
using Mango.Services.OrderApi.Data;
using Mango.Services.OrderApi.Service;
using Mango.Services.OrderApi.utility;
using Mango.Services.ShoppingCartApi.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersApi"));
});
IMapper mapper = MappingProfile.Mapper().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAzureMessageBus, AzureMessageBus>();

builder.Services.AddHttpClient("Product",
    (u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])))
    .AddHttpMessageHandler<BackendApiAuthHttpClientHandler>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthHttpClientHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AppAuthenticationExt();
builder.Services.AddAuthorization();

Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
