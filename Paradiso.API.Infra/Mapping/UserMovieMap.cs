namespace Paradiso.API.Infra.Mapping;

public class UserMovieMap : IEntityTypeConfiguration<UserMovie>
{
    public void Configure(EntityTypeBuilder<UserMovie> builder)
    {
        builder.ToTable("UserMovie")
           .HasKey(e => e.Id);

        builder.Property(x => x.IsOwner).HasColumnType("bit");

        builder.HasOne(e => e.User)
           .WithMany(e => e.UserMovies)
           .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Movie)
           .WithMany(e => e.UserMovies)
           .HasForeignKey(e => e.MovieId);
    }
}
