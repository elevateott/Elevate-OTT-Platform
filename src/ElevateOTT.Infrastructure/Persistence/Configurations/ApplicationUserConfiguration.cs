namespace ElevateOTT.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    #region Public Methods

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Each User can have many UserClaims
        builder.HasMany(u => u.Claims)
            .WithOne(u => u.User)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        // Each User can have many UserLogins
        builder.HasMany(u => u.Logins)
            .WithOne(u => u.User)
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        // Each User can have many UserTokens
        builder.HasMany(u => u.Tokens)
            .WithOne(u => u.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany(u => u.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.HasIndex(u => u.NormalizedUserName)
            .IsUnique(false);

        builder.Property(u => u.NormalizedUserName)
            .IsRequired();

        builder.HasIndex(u => u.NormalizedEmail)
            .IsUnique(false);

        builder.Property(u => u.NormalizedEmail)
            .IsRequired();

        // builder.Ignore(x => x.FullName);
    }

    #endregion Public Methods
}
