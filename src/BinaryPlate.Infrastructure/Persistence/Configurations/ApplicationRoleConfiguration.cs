namespace BinaryPlate.Infrastructure.Persistence.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    #region Public Methods

    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        // Each Role can have many entries in the UserRole join table
        builder.HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        // Each Role can have many associated RoleClaims
        builder.HasMany(e => e.RoleClaims)
            .WithOne(e => e.Role)
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique(false);

        builder.Property(r => r.NormalizedName)
            .IsRequired();
    }

    #endregion Public Methods
}