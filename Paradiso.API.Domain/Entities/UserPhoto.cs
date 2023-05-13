namespace Paradiso.API.Domain.Entities;

public class UserPhoto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PhotoId { get; set; }
    public bool IsOwner { get; set; }

    public virtual User User { get; set; }
    public virtual Photo Photo { get; set; }
}
