namespace Paradiso.API.Domain.Entities;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public Guid StateId { get; set; }


    public virtual State State { get; set; }


    public virtual ICollection<User> Users { get; set; }
}
