using Application.Courses.Dto;
using Application.Courses.Vm;
using Application.Interfaces;
using Application.Modules;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Services;

public class CourseService(IApplicationDbContext dbContext) : ICourseService
{
    public async Task<Guid> CreateCourse(CourseDto request, CancellationToken cancellationToken = default)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            ImagePath = request.ImagePath,
            CreatedAt = DateTime.UtcNow
        };
        
        await dbContext.Courses.AddAsync(course, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return course.Id;
    }

    public async Task UpdateCourse(Guid courseId, CourseDto request, CancellationToken cancellationToken = default)
    {
        var course = await dbContext.Courses.FindAsync([courseId], cancellationToken);
        
        if (course == null)
            throw new Exception("Курс не найден");
        
        course.Title = request.Title;
        course.Description = request.Description;
        course.ImagePath = request.ImagePath;
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCourse(Guid courseId, CancellationToken cancellationToken = default)
    {
        var course = await dbContext.Courses.FindAsync([courseId], cancellationToken);
        
        if (course == null)
            throw new Exception("Курс не найден");
        
        dbContext.Courses.Remove(course);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CourseGetAllVm>> GetAllCourses(CancellationToken cancellationToken = default)
    {
        var courses = await dbContext.Courses
            .AsNoTracking()   
            .Select(x => new CourseGetAllVm
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImagePath = x.ImagePath,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);
        
        return courses;
    }

    public async Task<CourseGetByIdVm> GetCourse(Guid courseId, CancellationToken cancellationToken = default)
    {
        var course = await dbContext.Courses
            .AsNoTracking()
            .Include(x => x.Modules)
            .Select(x => new CourseGetByIdVm
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImagePath = x.ImagePath,
                CreatedAt = x.CreatedAt,
                Modules = x.Modules.OrderBy(module => module.Order).Select(m => new ModuleGetAllVm
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    VideoPath = m.VideoPath,
                    Order = m.Order
                }).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == courseId, cancellationToken);
        
        if (course == null)
            throw new Exception("Курс не найден");
        
        return course;
    }
}