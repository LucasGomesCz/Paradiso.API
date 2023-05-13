namespace Paradiso.API.Infra.Mapping;

public class GenreMap : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genre")
           .HasKey(x => x.Id);

        builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(5000);
    }
}
