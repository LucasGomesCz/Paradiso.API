namespace Paradiso.API.Infra.Mapping;

public class UserSoundTrackMap : IEntityTypeConfiguration<UserSoundTrack>
{
    public void Configure(EntityTypeBuilder<UserSoundTrack> builder)
    {
        builder.ToTable("UserSoundTrack")
           .HasKey(e => e.Id);

        builder.Property(x => x.IsOwner).HasColumnType("bit");

        builder.HasOne(e => e.User)
          .WithMany(e => e.UserSoundTracks)
          .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.SoundTrack)
          .WithMany(e => e.UserSoundTracks)
          .HasForeignKey(e => e.SoundTrackId);
    }
}
