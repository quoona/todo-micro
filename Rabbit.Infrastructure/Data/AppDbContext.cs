using Microsoft.EntityFrameworkCore;
using Rabbit.Domain.Entities;
using Rabbit.Infrastructure.EntityConfigurations;

namespace Rabbit.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
    }

    public DbSet<Todo> Todos => Set<Todo>();
}