using Application.Users.Dto;
using Application.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : BaseController
{
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute] Guid id, 
        [FromBody] UserDto userDto, CancellationToken cancellationToken = default)
    {
        await userService.UpdateUserAsync(id, userDto, cancellationToken);

        return ResponseOk();
    }
}