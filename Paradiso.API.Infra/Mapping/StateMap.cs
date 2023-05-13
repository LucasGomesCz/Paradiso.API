namespace Paradiso.API.Infra.Mapping;

public class StateMap : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("State")
            .HasKey(x => x.Id);

        builder.Property(e => e.Name).HasColumnType("varchar").HasMaxLength(1000);
    }
}
