namespace Paradiso.API.Infra.Mapping;

public class PhotoMap : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("Photo")
            .HasKey(e => e.Id);

        builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(x => x.ReleaseDate).HasColumnType("datetime2");
        builder.Property(x => x.HasCopyright).HasColumnType("bit");
        builder.Property(x => x.Description).HasColumnType("text").IsRequired(false);
        builder.Property(x => x.HashCode).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(x => x.Extension).HasColumnType("varchar").HasMaxLength(10);
        builder.Property(x => x.Url).HasColumnType("varchar").HasMaxLength(1000);

        builder.HasOne(e => e.Genre)
           .WithMany(e => e.Photos)
           .HasForeignKey(e => e.GenreId);
    }
}
