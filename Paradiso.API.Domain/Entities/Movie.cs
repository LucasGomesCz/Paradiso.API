namespace Paradiso.API.Domain.Entities;

public class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan LengthTime { get; set; }
    public bool HasCopyright { get; set; }
    public string? Description { get; set; }

    public string HashCode { get; set; }
    public string Extension { get; set; }
    public string Url { get; set; }

    public Guid KindMovieId { get; set; }
    public Guid GenreId { get; set; }

    public virtual KindMovie KindMovie { get; set; }
    public virtual Genre Genre { get; set; }

    public virtual ICollection<UserMovie> UserMovies { get; set; }
}
