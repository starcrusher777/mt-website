using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MT.Application.Services;
using MT.Domain.Interfaces;
using MT.Infrastructure.Data;
using MT.Infrastructure.Data.Repositories;
using MT.Infrastructure.Maps.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MerchTrade;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        builder.Services.AddAuthorization();
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<OrderService>();
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<UserService>();
        
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<ItemService>();
        
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<AuthService>();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddAutoMapper(typeof(OrderCreateFormProfile));
        builder.Services.AddAutoMapper(typeof(UserProfile));
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        var app = builder.Build();
        
        app.UseCors("AllowFrontend");
        app.UseAuthentication();
        app.UseAuthorization();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseStaticFiles();
        app.MapControllers();
        app.UseAuthorization();
        
        app.Run();
    }
}