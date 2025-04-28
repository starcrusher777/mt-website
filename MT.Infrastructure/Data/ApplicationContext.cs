using Microsoft.EntityFrameworkCore;
using MT.Domain.Entities;

namespace MT.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<ItemImageEntity> ItemImages { get; set; }
}