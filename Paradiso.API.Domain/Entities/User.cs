using Paradiso.API.Domain.Entities;
using Paradiso.API.Domain.Enums;

namespace Paradiso.API.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public EGender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public bool IsCreator { get; set; }
    public string? Telephone { get; set; }
    public string? Description { get; set; }


    public Guid AreaId { get; set; }
    public Guid CityId { get; set; }

    
    public virtual Area Area { get; set; }
    public virtual City City { get; set; }


    public virtual ICollection<UserSoundTrack> UserSoundTracks { get; set; }
    public virtual ICollection<UserMovie> UserMovies { get; set; }
    public virtual ICollection<UserPhoto> UserPhotos { get; set; }
    public virtual ICollection<UserScript> UserScripts { get; set; }
}
