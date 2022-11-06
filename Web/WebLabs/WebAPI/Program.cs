using DBLib.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TrendLineLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    //c.DescribeAllEnumsAsStrings();
});

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
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "api";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
