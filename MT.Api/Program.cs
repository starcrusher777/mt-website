using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MT.Application.Services;
using MT.Domain.Interfaces;
using MT.Infrastructure.Data;
using MT.Infrastructure.Data.Repositories;
using MT.Infrastructure.Maps.Profiles;

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
                    .AllowAnyHeader();
            });
        });
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddAutoMapper(typeof(OrderCreateFormProfile));
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAnyOrigin");
        }
        
        app.UseCors("AllowFrontend");
        app.UseStaticFiles();
        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        app.Run();
    }
}