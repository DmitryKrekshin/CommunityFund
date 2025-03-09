using BUKEP.CommunityFund.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BUKEP.CommunityFund.WebApi;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private const string UnhandledExceptionMessage = "Unhandled exception";

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        try
        {
            var users = await userService.GetAsync(user => true, cancellationToken);

            return Ok(users.ToList());
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new AddUser
            {
                PersonGuid = model.PersonGuid,
                Login = model.Login,
                Password = model.Password
            };

            var addedUser = await userService.AddAsync(newUser, cancellationToken);
            return Ok(addedUser);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    [Route("api/v1/[controller]/[action]")]
    public async Task<IActionResult> ChangePassword(Guid userGuid, [FromBody] ChangePasswordModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await userService.ChangePasswordAsync(userGuid, model.NewPassword, cancellationToken);
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    [Route("api/v1/[controller]/[action]")]
    public async Task<IActionResult> BlockUser(Guid userGuid, CancellationToken cancellationToken = default)
    {
        try
        {
            await userService.BlockAsync(userGuid, cancellationToken);
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }
    
    [HttpPost]
    [Route("api/v1/[controller]/[action]")]
    public async Task<IActionResult> UnblockUser(Guid userGuid, CancellationToken cancellationToken = default)
    {
        try
        {
            await userService.UnblockAsync(userGuid, cancellationToken);
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }
}