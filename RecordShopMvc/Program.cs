using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecordShopMvc.Data;
using RecordShopMvc.Models.Entities;
using RecordShopMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AuthDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AuthDbContext>();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/auth/signin";


});
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();



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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Products}/{id?}");

app.Run();
