using CryptoService.Data.Database;
using CryptoService.Data.Repositories;
using CryptoService.Data.Repositories.Interfaces;
using CryptoService.Integrations.CoinApi.Services;
using CryptoService.Integrations.CoinApi.Services.Interfaces;
using CryptoService.Logic.Services;
using CryptoService.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<ICryptoDataProvider, CryptoDataProvider>();
builder.Services.AddHostedService<UpdatePriceHostedService>();

builder.Services.AddSingleton<ICoinApiService, CoinApiService>(x => 
    new CoinApiService(configuration.GetSection("Integrations:CoinApiKey").Value)
);

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<CryptoDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();