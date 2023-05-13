namespace Paradiso.API.Domain.Entities;

public class UserSoundTrack
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SoundTrackId { get; set; }
    public bool IsOwner { get; set; }

    public virtual User User { get; set; }
    public virtual SoundTrack SoundTrack { get; set; }
}
