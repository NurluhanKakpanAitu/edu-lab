using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Module> Modules { get; set; }
    
    public DbSet<PracticeWork> PracticeWorks { get; set; }
}