namespace Paradiso.API.Domain.Entities;

public class State
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<City> Cities { get; set; }
}
