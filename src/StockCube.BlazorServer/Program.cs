using StockCube.UI;
using MudBlazor.Services;
using StockCube.WebAPI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddStockCubeDomainModel();
builder.Services.AddStockCubeInfrastructure(builder.Configuration);
builder.Services.AddWebAPI();
builder.Services.AddStockCubeUI();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["web:client_id"] ?? "";
        options.ClientSecret = builder.Configuration["web:client_secret"] ?? "";
        options.SaveTokens = true;
        options.ReturnUrlParameter = "http://localhost:5000/_Host";
    });

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
