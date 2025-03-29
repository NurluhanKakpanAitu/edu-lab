using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .OwnsOne(x => x.Title, pr => pr.ToJson());
        
        modelBuilder.Entity<Course>()
            .OwnsOne(x => x.Description, pr => pr.ToJson());
        
        modelBuilder.Entity<Module>()
            .OwnsOne(x => x.Title, pr => pr.ToJson());
        
        modelBuilder.Entity<Module>()
            .OwnsOne(x => x.Description, pr => pr.ToJson());

        modelBuilder.Entity<PracticeWork>()
            .OwnsOne(x => x.Title, pr => pr.ToJson());
        
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<User> Users { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Module> Modules { get; set; }
    
    public DbSet<PracticeWork> PracticeWorks { get; set; }
}