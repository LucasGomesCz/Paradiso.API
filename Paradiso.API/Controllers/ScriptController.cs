namespace Paradiso.API.Controllers;

[ApiController]
public class ScriptController : ControllerBase
{
    private readonly IScriptHandler _handler;

    public ScriptController(IScriptHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("Script")]
    public async Task<IActionResult> GetAsync([FromQuery] ScriptGetParams @params) => Ok(await _handler.GetAsync(@params));

    [HttpPost("Script")]
    public async Task<IActionResult> UploadAsync([FromBody] ScriptPostParams @params) => Ok(await _handler.UploadAsync(@params));

    [HttpPut("Script")]
    public async Task<IActionResult> UpdateAsync([FromBody] ScriptPutParams @params) => Ok(await _handler.UpdateAsync(@params));

    [HttpDelete("Script")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteParams @params) => Ok(await _handler.DeleteAsync(@params));
}
