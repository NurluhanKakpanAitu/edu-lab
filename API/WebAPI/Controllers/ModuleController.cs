using System.Diagnostics;
using Application.Modules;
using Application.Modules.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
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
    
    [HttpPost("run")]
    public IActionResult RunCode([FromBody] CodeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
        {
            return BadRequest(new { error = "Code cannot be empty." });
        }

        try
        {
            // Create a temporary Python file
            var tempFileName = Path.GetTempFileName() + ".py";
            System.IO.File.WriteAllText(tempFileName, request.Code);

            // Execute the Python file
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "python3", 
                    Arguments = tempFileName,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            // Capture output and errors
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            // Delete the temporary file
            System.IO.File.Delete(tempFileName);

            return Ok(new
            {
                output, error
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
public class CodeRequest
{
    public string Code { get; set; }
}