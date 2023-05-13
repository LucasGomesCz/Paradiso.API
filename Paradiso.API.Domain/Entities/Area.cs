using Paradiso.API.Domain.Entities;

namespace Paradiso.API.Domain.Entities;

public class Area
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
