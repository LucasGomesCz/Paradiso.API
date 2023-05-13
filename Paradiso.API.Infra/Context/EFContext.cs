using Microsoft.EntityFrameworkCore;

namespace Paradiso.API.Infra.Context;

public class EFContext : DbContext
{
    public EFContext() { }

    public EFContext(DbContextOptions<EFContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFContext).Assembly);
}
