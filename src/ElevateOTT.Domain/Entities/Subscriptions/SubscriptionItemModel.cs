using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Entities.Subscriptions;

[Table("SubscriptionItems")]
public class SubscriptionItemModel : BaseEntity
{
    #region Chargebee Properties

    public string ItemPriceId { get; set; } = string.Empty;

    public ProductItemType ItemType { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal Amount { get; set; }

    public int FreeQuantity { get; set; }

    public string Object { get; set; } = string.Empty;

    #endregion

    #region foreign keys
    [ForeignKey(nameof(SubscriptionModel))]
    public Guid? SubscriptionId { get; set; }

    #endregion

    #region navigational properties
    public SubscriptionModel? Subscription { get; set; }
    #endregion
}
