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
        
        modelBuilder.Entity<Test>()
            .OwnsOne(x => x.Title, pr => pr.ToJson());
        
        modelBuilder.Entity<Question>()
            .OwnsOne(x => x.Title, pr =>
            {
                pr.Property(x => x.En).HasColumnName("TitleEn");
                
                pr.Property(x => x.Ru).HasColumnName("TitleRu");
                
                pr.Property(x => x.Kz).HasColumnName("TitleKz");
            });
        
        modelBuilder.Entity<Answer>()
            .OwnsOne(x => x.Title, pr =>
            {
                pr.Property(x => x.En).HasColumnName("TitleEn");
                
                pr.Property(x => x.Ru).HasColumnName("TitleRu");
                
                pr.Property(x => x.Kz).HasColumnName("TitleKz");
            });

        modelBuilder.Entity<PracticeWork>()
            .OwnsOne(x => x.Title, pr => pr.ToJson());
        
        modelBuilder.Entity<PracticeWork>()
            .OwnsOne(x => x.Description, pr =>
            {
                pr.Property(x => x.En).HasColumnName("DescriptionEn");
                
                pr.Property(x => x.Ru).HasColumnName("DescriptionRu");
                
                pr.Property(x => x.Kz).HasColumnName("DescriptionKz");
            });
        
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<User> Users { get; set; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Module> Modules { get; set; }
    
    public DbSet<Test> Tests { get; set; }
    
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<Answer> Answers { get; set; }
    
    public DbSet<PracticeWork> PracticeWorks { get; set; }
}