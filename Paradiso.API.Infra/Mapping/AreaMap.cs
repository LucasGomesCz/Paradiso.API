namespace Paradiso.API.Infra.Mapping;

public class AreaMap : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable("Area")
            .HasKey(e => e.Id);

        builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(x => x.Description).HasColumnType("text");
    }
}
