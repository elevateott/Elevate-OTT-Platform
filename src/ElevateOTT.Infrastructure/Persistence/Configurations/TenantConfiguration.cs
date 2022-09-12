namespace ElevateOTT.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    #region Public Methods

    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasIndex(p => p.FullName).IsUnique();
        builder.Property(e => e.Id).ValueGeneratedNever();
    }

    #endregion Public Methods
}