using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using GeekShopping.ProductAPI.Config;
using AutoMapper;
using GeekShopping.ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// AutoMapper 15+: registrar Profile diretamente
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingConfig>();
});

builder.Services.AddScoped<IProductRespository, ProductRepository>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
