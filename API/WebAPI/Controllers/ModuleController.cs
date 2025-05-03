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

        // Use the application's base directory for temp files
        var tempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");

        // Create temp directory if it doesn't exist
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        try
        {
            // Test if Python is available
            var pythonCheck = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "which",
                    Arguments = "python3",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            
            pythonCheck.Start();
            pythonCheck.WaitForExit();
            var pythonPath = pythonCheck.StandardOutput.ReadToEnd().Trim();
            
            if (string.IsNullOrEmpty(pythonPath))
            {
                return StatusCode(500, new { error = "Python3 is not installed or not in PATH" });
            }

            // Create a temporary Python file with unique name
            var tempFileName = Path.Combine(tempDir, $"{Guid.NewGuid()}.py");
            System.IO.File.WriteAllText(tempFileName, request.Code);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = tempFileName,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = tempDir
                }
            };

            try
            {
                process.Start();

                if (!process.WaitForExit(10000)) // 10 second timeout
                {
                    process.Kill();
                    return StatusCode(408, new { error = "Execution timeout" });
                }

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                return Ok(new { output, error });
            }
            finally
            {
                // Cleanup
                try
                {
                    if (System.IO.File.Exists(tempFileName))
                    {
                        System.IO.File.Delete(tempFileName);
                    }
                }
                catch
                {
                    // Log cleanup errors but don't fail the request
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Execution error: {ex.Message}" });
        }
    }
}
public class CodeRequest
{
    public string Code { get; set; }
}