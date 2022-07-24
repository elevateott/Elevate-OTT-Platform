namespace ElevateOTT.Domain.Entities.Products;

[Table("ProductItems")]
public class ProductItemModel : BaseEntity
{
    #region Chargebee Properties
    public ProductItemType Type { get; set; }

    public string ItemId { get; set; } = string.Empty;

    public string ItemFamilyId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string ExternalName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ItemStatus Status { get; set; }

    public string ChargeModel { get; set; } = string.Empty;

    public int FreeQuantity { get; set; }

    public string Object { get; set; } = string.Empty;

    public string CurrencyCode { get; set; } = string.Empty;

    public bool EnabledInHostedPages { get; set; }

    public bool EnabledInPortal { get; set; }

    public bool EnableForCheckout { get; set; }

    public bool Metered { get; set; }

    public bool Archivable { get; set; }

    public bool Taxable { get; set; }

    public string Channel { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    public string UsageCalculation { get; set; } = string.Empty;

    public bool IsShippable { get; set; }

    public bool IsGiftable { get; set; }

    public long ResourceVersion { get; set; }

    public string RedirectUrl { get; set; } = string.Empty;

    public string ItemApplicability { get; set; } = string.Empty;
    #endregion

    public string Title { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string LongDescription { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public SubscriptionAvailability SubscriptionAvailability { get; set; }

    #region foreign keys
    [ForeignKey(nameof(ProductFamilyModel))]
    public Guid? ProductFamilyId { get; set; }
    #endregion

    #region navigational properties
    public ProductFamilyModel? ProductFamily { get; set; }
    public List<ItemPriceModel>? ItemPrices { get; set; }
    #endregion
}
