namespace Paradiso.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserHandler _handler;

    public UserController(IUserHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("User")]
    public async Task<IActionResult> GetAsync([FromQuery] UserGetParams @params) => Ok(await _handler.GetAsync(@params));

    [HttpPost("User")]
    public async Task<IActionResult> UploadAsync([FromBody] UserPostParams @params) => Ok(await _handler.UploadAsync(@params));

    [HttpPut("User")]
    public async Task<IActionResult> UpdateAsync([FromBody] UserPutParams @params) => Ok(await _handler.UpdateAsync(@params));

    [HttpDelete("User")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteParams @userId) => Ok(await _handler.DeleteAsync(@userId));
}
