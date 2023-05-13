namespace Paradiso.API.Controllers;

[ApiController]
public class PhotoController : ControllerBase
{
    private readonly IPhotoHandler _handler;

    public PhotoController(IPhotoHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("Photo")]
    public async Task<IActionResult> GetAsync([FromQuery] PhotoGetParams @params) => Ok(await _handler.GetAsync(@params));

    [HttpPost("Photo")]
    public async Task<IActionResult> UploadAsync([FromBody] PhotoPostParams @params) => Ok(await _handler.UploadAsync(@params));

    [HttpPut("Photo")]
    public async Task<IActionResult> UpdateAsync([FromBody] PhotoPutParams @params) => Ok(await _handler.UpdateAsync(@params));

    [HttpDelete("Photo")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteParams @params) => Ok(await _handler.DeleteAsync(@params));
}
