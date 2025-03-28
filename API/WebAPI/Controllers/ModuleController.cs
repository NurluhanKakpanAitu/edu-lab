using Application.Modules;
using Application.Modules.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ModuleController(IModuleService moduleService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ModuleDto moduleDto, CancellationToken cancellationToken = default)
    {
        var id = await moduleService.CreateAsync(moduleDto, cancellationToken);
        return ResponseOk(id);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ModuleDto moduleDto, CancellationToken cancellationToken = default)
    {
        await moduleService.UpdateAsync(id, moduleDto, cancellationToken);
        return ResponseOk();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await moduleService.DeleteAsync(id, cancellationToken);
        return ResponseOk();
    }
    
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetModule([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var module = await moduleService.GetModule(id, cancellationToken);
        return ResponseOk(module);
    }
}