using Application.Users.Dto;
using Application.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : BaseController
{
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute] Guid id, 
        [FromBody] UserDto userDto, CancellationToken cancellationToken = default)
    {
        await userService.UpdateUserAsync(id, userDto, cancellationToken);

        return ResponseOk();
    }
    
    [HttpGet("{testId:guid}/test-result")]
    [Authorize]
    public async Task<IActionResult> GetUserTestResultAsync(
        [FromRoute] Guid testId, CancellationToken cancellationToken = default)
    {
        var result = await userService.GetUserTestResultAsync(testId, cancellationToken);

        return ResponseOk(result);
    }
}