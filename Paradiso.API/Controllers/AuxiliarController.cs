namespace Paradiso.API.Controllers;

[ApiController]
public class AuxiliarController : ControllerBase
{
    private readonly IAuxiliarHandler _handler;

    public AuxiliarController(IAuxiliarHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("City")]
    public async Task<IActionResult> GetAsync([FromQuery] CityGetParams @params) => Ok(await _handler.GetCityAsync(@params));

    [HttpGet("State")]
    public async Task<IActionResult> GetAsync([FromQuery] StateGetParams @params) => Ok(await _handler.GetStateAsync(@params));

    [HttpGet("Area")]
    public async Task<IActionResult> GetAsync([FromQuery] AreaGetParams @params) => Ok(await _handler.GetAreaAsync(@params));

    [HttpGet("Genre")]
    public async Task<IActionResult> GetAsync([FromQuery] GenreGetParams @params) => Ok(await _handler.GetGenreAsync(@params));

    [HttpGet("KindMovie")]
    public async Task<IActionResult> GetAsync([FromQuery] KindMovieGetParams @params) => Ok(await _handler.GetKindMovieAsync(@params));
}
