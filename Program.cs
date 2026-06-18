using InvestmentTracker_backend;
using InvestmentTracker_backend.Repositories;
using InvestmentTracker_backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<StockRepository>();
builder.Services.AddScoped<StockService>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UsersRepository>();

builder.Services.AddScoped<StockPositionsRepository>();
builder.Services.AddScoped<StockPositionsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseSwaggerUI(c=> 
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Investment Tracker API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();