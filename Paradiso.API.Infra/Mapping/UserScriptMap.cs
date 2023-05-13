namespace Paradiso.API.Infra.Mapping;

public class UserScriptMap : IEntityTypeConfiguration<UserScript>
{
    public void Configure(EntityTypeBuilder<UserScript> builder)
    {
        builder.ToTable("UserScript")
           .HasKey(e => e.Id);

        builder.Property(x => x.IsOwner).HasColumnType("bit");

        builder.HasOne(e => e.User)
           .WithMany(e => e.UserScripts)
           .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Script)
           .WithMany(e => e.UserScripts)
           .HasForeignKey(e => e.ScriptId);
    }
}
