using Microsoft.AspNetCore.Http;

namespace Paradiso.API.Domain.Models.SoundTracks;

public class SoundTrackPostParams
{
    public Guid UserId { get; set; }
    public List<Guid>? Cast { get; set; }


    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public bool HasCopyright { get; set; }
    public string? Description { get; set; }
    public Guid KindMovieId { get; set; }
    public Guid GenreId { get; set; }


    public IFormFile File { get; set; }
}
