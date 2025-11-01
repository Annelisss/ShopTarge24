using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();

builder.Services.AddScoped<IFileServices, FileServices>();

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<IKindergartenService, KindergartenService>();
builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();

builder.Services.AddHttpClient<IChuckNorrisServices, ChuckNorrisServices>();
builder.Services.AddHttpClient<ICocktailServices, CocktailServices>();

builder.Services.AddDbContext<ShopTARge24Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
