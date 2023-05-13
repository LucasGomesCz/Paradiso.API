namespace Paradiso.API.Domain.Entities;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<SoundTrack> SoundTracks { get; set; }
    public virtual ICollection<Movie> Movies { get; set; }
    public virtual ICollection<Photo> Photos { get; set; }
    public virtual ICollection<Script> Scripts { get; set; }
}
