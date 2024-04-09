using MangoWeb.BaseService;
using MangoWeb.Service;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SD.CouponAPIbase = builder.Configuration["ServiceUrls:CouponApi"];
SD.AuthAPIbase = builder.Configuration["ServiceUrls:AuthAPI"];
SD.ProductAPIbase = builder.Configuration["ServiceUrls:ProductAPI"];
SD.ShoppingCartAPIbase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
SD.OrderAPIbase = builder.Configuration["ServiceUrls:OrderAPI"];

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<ICouponService, CouponService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<IOrderService, OrderService>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ITokenProvider , TokenProvider>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(Options =>
    {
        Options.ExpireTimeSpan = TimeSpan.FromHours(10);
        Options.LoginPath = "/Auth/Login";
        Options.AccessDeniedPath = "/Auth/AccessDenied";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
