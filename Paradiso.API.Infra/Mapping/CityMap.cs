namespace Paradiso.API.Infra.Mapping;

public class CityMap : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("City")
            .HasKey(e => e.Id);

        builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(5000);

        builder.HasOne(e => e.State)
            .WithMany(e => e.Cities)
            .HasForeignKey(e => e.StateId);
    }
}
