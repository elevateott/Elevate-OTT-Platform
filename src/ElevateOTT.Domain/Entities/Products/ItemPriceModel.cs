namespace ElevateOTT.Domain.Entities.Products;

[Table("ItemPrices")]
public class ItemPriceModel : EntityBase
{
    #region Chargebee Properties
    public string ChargebeeItemPriceId { get; set; } = string.Empty;

    public string ItemId { get; set; } = string.Empty;

    public string ItemFamilyId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public ItemStatus Status { get; set; }

    public string ExternalName { get; set; } = string.Empty;

    public string PricingModel { get; set; } = string.Empty;

    public long ResourceVersion { get; set; }

    public bool Archivable { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }

    public string PricingInDecimal { get; set; } = string.Empty;

    public int Period { get; set; }

    public BillingPeriodUnit PeriodUnit { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public string Object { get; set; } = string.Empty;

    public int FreeQuantity { get; set; }

    public string FreeQuantityInDecimal { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public bool IsTaxable { get; set; }

    public ProductItemType ProductItemType { get; set; }

    public bool ShowDescriptionInInvoices { get; set; }

    public bool ShowDescriptionInQuotes { get; set; }
    #endregion


    #region foreign keys
    [ForeignKey(nameof(ProductItemModel))]
    public Guid? ProductItemId { get; set; }
    #endregion

    #region navigational properties
    public ProductItemModel? ProductItem { get; set; }
    #endregion
}
