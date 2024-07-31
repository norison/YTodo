using Microsoft.EntityFrameworkCore;
using YTodo.Persistence.Entities;

namespace YTodo.Persistence;

public class YTodoDbContext(DbContextOptions<YTodoDbContext> options) : DbContext(options)
{
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<TodoEntity> Todos => Set<TodoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YTodoDbContext).Assembly);
    }
}