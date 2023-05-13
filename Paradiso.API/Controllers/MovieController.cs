namespace Paradiso.API.Controllers;

[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieHandler _handler;

    public MovieController(IMovieHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("Movie")]
    public async Task<IActionResult> GetAsync([FromQuery] MovieGetParams @params) => Ok(await _handler.GetAsync(@params));

    [HttpPost("Movie")]
    public async Task<IActionResult> UploadAsync([FromBody] MoviePostParams @params) => Ok(await _handler.UploadAsync(@params));

    [HttpPut("Movie")]
    public async Task<IActionResult> UpdateAsync([FromBody] MoviePutParams @params) => Ok(await _handler.UpdateAsync(@params));

    [HttpDelete("Movie")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteParams @params) => Ok(await _handler.DeleteAsync(@params));
}
