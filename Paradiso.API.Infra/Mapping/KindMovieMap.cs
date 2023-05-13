namespace Paradiso.API.Infra.Mapping;

public class KindMovieMap : IEntityTypeConfiguration<KindMovie>
{
    public void Configure(EntityTypeBuilder<KindMovie> builder)
    {
        builder.ToTable("KindMovie")
           .HasKey(x => x.Id);

        builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(5000);
    }
}
