namespace ElevateOTT.Infrastructure.Persistence.Configurations;

public class UserAttachmentsConfiguration : IEntityTypeConfiguration<ApplicationUserAttachment>
{
    #region Public Methods

    public void Configure(EntityTypeBuilder<ApplicationUserAttachment> builder)
    {
        builder.ToTable("AspNetUserAttachments");

        builder.Property(t => t.FileUri).IsRequired();

        builder.Property(t => t.UserId).IsRequired();
    }

    #endregion Public Methods
}