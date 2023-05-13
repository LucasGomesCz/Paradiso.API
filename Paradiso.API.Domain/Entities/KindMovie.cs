namespace Paradiso.API.Domain.Entities;

public class KindMovie
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Movie> Movies { get; set; }
}
