using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radao.Data;
using Radao.Services.ServicesInterfaces;
using Radao.Services;
using Radao.Mapper; // Ensure this namespace is correct

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RadaoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RadaoDB")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddScoped<IFountainService, FountainService>();
builder.Services.AddScoped<FountainMapper>(); // Register FountainMapper
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<DeviceMapper>(); // Register DeviceMapper

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