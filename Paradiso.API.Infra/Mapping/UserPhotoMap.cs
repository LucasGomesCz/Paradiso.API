namespace Paradiso.API.Infra.Mapping;

public class UserPhotoMap : IEntityTypeConfiguration<UserPhoto>
{
    public void Configure(EntityTypeBuilder<UserPhoto> builder)
    {
        builder.ToTable("UserPhoto")
           .HasKey(e => e.Id);

        builder.Property(x => x.IsOwner).HasColumnType("bit");

        builder.HasOne(e => e.User)
           .WithMany(e => e.UserPhotos)
           .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Photo)
           .WithMany(e => e.UserPhotos)
           .HasForeignKey(e => e.PhotoId);
    }
}
