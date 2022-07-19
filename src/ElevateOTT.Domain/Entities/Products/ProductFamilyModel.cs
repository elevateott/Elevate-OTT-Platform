namespace ElevateOTT.Domain.Entities.Products;

[Table("ProductFamilies")]
public class ProductFamilyModel : EntityBase
{
    public string ChargebeeItemFamilyId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ProductFamilyStatus Status { get; set; }

    public long ResourceVersion { get; set; }

    public string Object { get; set; } = string.Empty;

    #region foreign keys
    //[ForeignKey(nameof(TenantModel))]
    //public Guid? TenantId { get; set; }
    #endregion

    #region navigational properties
    //public TenantModel? Tenant { get; set; }
    public List<ProductItemModel>? ProductItems { get; set; }
    #endregion
}
