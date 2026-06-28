using System.Text;
using InvestmentTracker_backend;
using InvestmentTracker_backend.Repositories;
using InvestmentTracker_backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer; // 👈 Musisz to dodać
using Microsoft.IdentityModel.Tokens;   
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

builder.Services.AddScoped<DividendRepository>();
builder.Services.AddScoped<DividendService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost"; 
        options.Challenge = JwtBearerDefaults.AuthenticationScheme;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JwtConfig:Issuer"], 
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"] ?? ""))
        };
    });

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseSwaggerUI(c=> 
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Investment Tracker API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();