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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
        
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Contacts)
            .WithOne(c => c.User)
            .HasForeignKey<ContactsEntity>(c => c.UserId);
        
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Socials)
            .WithOne(s => s.User)
            .HasForeignKey<SocialsEntity>(s => s.UserId);
        
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Personals)
            .WithOne(p => p.User)
            .HasForeignKey<PersonalsEntity>(p => p.UserId);
    }
}