namespace ElevateOTT.Domain.Entities.Content;

[Table("SubscriptionPlans")]

public class BundleModel : EntityBase
{
    // freebie
    // rental
    // fixed price

    public BundleType BundleType { get; set; }
}
