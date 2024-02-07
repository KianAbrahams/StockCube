using StockCube.UI;
using MudBlazor.Services;
using StockCube.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddStockCubeDomainModel();
builder.Services.AddStockCubeInfrastructure(builder.Configuration);
builder.Services.AddStockCubeUI();
builder.Services.AddWebAPI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
