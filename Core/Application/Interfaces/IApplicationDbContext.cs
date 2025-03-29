using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Module> Modules { get; set; }
    
    public DbSet<PracticeWork> PracticeWorks { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}