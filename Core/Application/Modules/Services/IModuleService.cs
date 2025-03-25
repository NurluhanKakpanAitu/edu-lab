namespace Application.Modules.Services;

public interface IModuleService
{
    Task<Guid> CreateAsync(ModuleDto moduleDto, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(Guid id, ModuleDto moduleDto, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<ModuleGetAllVm>> GetAllAsync(Guid courseId, CancellationToken cancellationToken = default);
}