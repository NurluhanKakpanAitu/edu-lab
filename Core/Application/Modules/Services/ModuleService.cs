using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Modules.Services;

public class ModuleService(IApplicationDbContext dbContext) : IModuleService
{
    public async Task<Guid> CreateAsync(ModuleDto moduleDto, CancellationToken cancellationToken = default)
    {
        var existsCourse = await dbContext.Courses.AnyAsync(x => x.Id == moduleDto.CourseId, cancellationToken);
        
        if (!existsCourse)
            throw new Exception("Course not found");

        var module = new Module
        {
            Title = moduleDto.Title,
            Description = moduleDto.Description,
            VideoPath = moduleDto.VideoPath,
            Order = moduleDto.Order,
            CourseId = moduleDto.CourseId,
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
        
        await dbContext.Modules.AddAsync(module, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return module.Id;
    }

    public async Task UpdateAsync(Guid id, ModuleDto moduleDto, CancellationToken cancellationToken = default)
    {
         var module = await dbContext.Modules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
         
            if (module == null)
                throw new Exception("Module not found");
            
            module.Title = moduleDto.Title;
            module.Description = moduleDto.Description;
            module.VideoPath = moduleDto.VideoPath;
            module.Order = moduleDto.Order;
            module.CourseId = moduleDto.CourseId;
            
            await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var module = await dbContext.Modules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
         
        if (module == null)
            throw new Exception("Module not found");
        
        dbContext.Modules.Remove(module);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ModuleGetAllVm>> GetAllAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Modules
            .AsNoTracking()
            .Where(x => x.CourseId == courseId)
            .Select(x => new ModuleGetAllVm
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                VideoPath = x.VideoPath,
                Order = x.Order,
                CourseId = x.CourseId,
                CreatedAt = x.CreatedAt
            })
            .OrderBy(x => x.Order)
            .ThenBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}