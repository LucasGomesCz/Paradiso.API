namespace Paradiso.API.Infra.Mapping;

public class SoundTrackMap : IEntityTypeConfiguration<SoundTrack>
{
    public void Configure(EntityTypeBuilder<SoundTrack> builder)
    {
        builder.ToTable("SoundTrack")
            .HasKey(e => e.Id);

        builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(x => x.ReleaseDate).HasColumnType("datetime2");
        builder.Property(x => x.LengthTime).HasColumnType("time");
        builder.Property(x => x.HasCopyright).HasColumnType("bit");
        builder.Property(x => x.Description).HasColumnType("text").IsRequired(false);
        builder.Property(x => x.HashCode).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(x => x.Extension).HasColumnType("varchar").HasMaxLength(10);
        builder.Property(x => x.Url).HasColumnType("varchar").HasMaxLength(1000);

        builder.HasOne(e => e.Genre)
           .WithMany(e => e.SoundTracks)
           .HasForeignKey(e => e.GenreId);
    }
}
