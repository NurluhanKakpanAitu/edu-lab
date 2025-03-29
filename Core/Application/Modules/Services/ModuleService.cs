using Application.Courses.Vm;
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
            CreatedAt = DateTime.UtcNow,
            TaskPath = moduleDto.TaskPath,
            PresentationPath = moduleDto.PresentationPath
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
            module.TaskPath = moduleDto.TaskPath;
            module.PresentationPath = moduleDto.PresentationPath;
            
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

    public async Task<ModuleVm> GetModule(Guid id, CancellationToken cancellationToken = default)
    {
        var module = await dbContext.Modules
            .Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (module == null)
            throw new Exception("Module not found");
        
        return new ModuleVm
        {
            Id = module.Id,
            Title = module.Title,
            Description = module.Description,
            VideoPath = module.VideoPath,
            Order = module.Order,
            CreatedAt = module.CreatedAt,
            TaskPath = module.TaskPath,
            PresentationPath = module.PresentationPath,
            CourseId = module.CourseId
        };
    }
}