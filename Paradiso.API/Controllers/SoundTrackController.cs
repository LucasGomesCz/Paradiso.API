namespace Paradiso.API.Controllers;

[ApiController]
public class SoundTrackController : ControllerBase
{
    private readonly ISoundTrackHandler _handler;

    public SoundTrackController(ISoundTrackHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("SoundTrack")]
    public async Task<IActionResult> GetAsync([FromQuery] SoundTrackGetParams @params) => Ok(await _handler.GetAsync(@params));

    [HttpPost("SoundTrack")]
    public async Task<IActionResult> UploadAsync([FromBody] SoundTrackPostParams @params) => Ok(await _handler.UploadAsync(@params));

    [HttpPut("SoundTrack")]
    public async Task<IActionResult> UpdateAsync([FromBody] SoundTrackPutParams @params) => Ok(await _handler.UpdateAsync(@params));

    [HttpDelete("SoundTrack")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteParams @params) => Ok(await _handler.DeleteAsync(@params));
}
