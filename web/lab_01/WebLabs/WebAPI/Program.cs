using DBLib.Models;
using TrendLineLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddScoped<IShopsRepository, PgSQLShopsRepository>();
builder.Services.AddScoped<IProductsRepository, PgSQLProductsRepository>();
builder.Services.AddScoped<ISaleReceiptsRepository, PgSQLSaleReceiptsRepository>();
builder.Services.AddScoped<ISaleReceiptPositionsRepository, PgSQLSaleReceiptPositionsRepository>();
builder.Services.AddScoped<ICostStoryRepository, PgSQLCostStoryRepository>();
builder.Services.AddScoped<ICostsRepository, PgSQLCostsRepository>();
builder.Services.AddScoped<IAvailabilityRepository, PgSQLAvailabilityRepository>();

builder.Services.AddSingleton<BaseTrendLine, PolynomialTrendLine>();

var app = builder.Build();

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
