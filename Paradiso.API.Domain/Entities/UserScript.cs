namespace Paradiso.API.Domain.Entities;

public class UserScript
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ScriptId { get; set; }
    public bool IsOwner { get; set; }

    public virtual User User { get; set; }
    public virtual Script Script { get; set; }
}
