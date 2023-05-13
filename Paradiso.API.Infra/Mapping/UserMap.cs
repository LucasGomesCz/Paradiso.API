namespace Paradiso.API.Infra.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User")
            .HasKey(e => e.Id);

        builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(x => x.Gender).HasConversion<short>();
        builder.Property(x => x.Birthday).HasColumnType("datetime");
        builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(x => x.IsCreator).HasColumnType("bit");
        builder.Property(x => x.Telephone).HasColumnType("varchar").HasMaxLength(20).IsRequired(false);
        builder.Property(x => x.Description).HasColumnType("text").IsRequired(false);

        builder.HasOne(e => e.Area)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.AreaId);

        builder.HasOne(e => e.City)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.CityId);
    }
}
