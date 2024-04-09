using AutoMapper;
using Mango.Azure.ServiceBus;
using Mango.Services.ShoppingCartApi;
using Mango.Services.ShoppingCartApi.AutoMapper;
using Mango.Services.ShoppingCartApi.Data;
using Mango.Services.ShoppingCartApi.Service;
using Mango.Services.ShoppingCartApi.utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCart"));
});
IMapper mapper = MappingProfile.Mapper().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductService , ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthHttpClientHandler>();

builder.Services.AddScoped<IAzureMessageBus,AzureMessageBus>();

builder.Services.AddHttpClient("Product",       
    (u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])))
    .AddHttpMessageHandler<BackendApiAuthHttpClientHandler>();
builder.Services.AddHttpClient("Coupon",
    (u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"])))
    .AddHttpMessageHandler<BackendApiAuthHttpClientHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the bearer Authorization string as following: 'Bearer Generated-JWT-Token'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            },new string[]{}

        }
    });
});

builder.AppAuthenticationExt(); 
builder.Services.AddAuthorization();

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
