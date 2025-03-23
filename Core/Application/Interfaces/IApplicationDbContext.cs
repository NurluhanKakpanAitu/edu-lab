using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Domain.Entities.User> Users { get; set; }
    
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Module> Modules { get; set; }
    
    public DbSet<Test> Tests { get; set; }
    
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<Answer> Answers { get; set; }
    
    public DbSet<PracticeWork> PracticeWorks { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}