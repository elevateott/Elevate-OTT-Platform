namespace BinaryPlate.Infrastructure.Persistence.Configurations;

public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    #region Public Methods

    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.HasKey(p => new { p.Id });
    }

    #endregion Public Methods
}