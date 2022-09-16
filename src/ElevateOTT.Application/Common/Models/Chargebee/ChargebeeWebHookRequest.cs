using System.Text.Json.Serialization;

namespace ElevateOTT.Application.Common.Models.Chargebee;

public class ChargebeeWebHookRequest
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("occurred_at")] public long OccurredAt { get; set; }

    [JsonPropertyName("source")] public string Source { get; set; } = string.Empty;

    [JsonPropertyName("user")] public string User { get; set; } = string.Empty;

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("api_version")] public string ApiVersion { get; set; } = string.Empty;

    [JsonPropertyName("event_type")] public string EventType { get; set; } = string.Empty;

    [JsonPropertyName("webhook_status")] public string WebhookStatus { get; set; } = string.Empty;

    [JsonPropertyName("content")] public Content? Content { get; set; }
}

public class Content
{
    [JsonPropertyName("customer")] public Customer? Customer { get; set; }

    [JsonPropertyName("subscription")] public Subscription? Subscription { get; set; }

    [JsonPropertyName("item")] public Item? Item { get; set; }

    [JsonPropertyName("item_family")] public ItemFamily? ItemFamily { get; set; }

    [JsonPropertyName("item_price")] public ItemPrice? ItemPrice { get; set; }

    #region Un-used properties
    [JsonPropertyName("plan")] public Plan? Plan { get; set; }
    [JsonPropertyName("addon")] public AddOn? AddOn { get; set; }
    [JsonPropertyName("attached_item")] public AttachedItem? AttachedItem { get; set; }
    [JsonPropertyName("payment_method")] public PaymentMethod? PaymentMethod { get; set; }
    [JsonPropertyName("card")] public Card? Card { get; set; }
    #endregion
}

public class Customer
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("first_name")] public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("last_name")] public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")] public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("company")] public string Company { get; set; } = string.Empty;

    [JsonPropertyName("auto_collection")] public string AutoCollection { get; set; } = string.Empty;

    [JsonPropertyName("net_term_days")] public int NetTermDays { get; set; }

    [JsonPropertyName("allow_direct_debit")]
    public bool AllowDirectDebit { get; set; }

    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }

    [JsonPropertyName("taxability")] public string Taxability { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("locale")] public string Locale { get; set; } = string.Empty;

    [JsonPropertyName("pii_cleared")] public string PiiCleared { get; set; } = string.Empty;

    [JsonPropertyName("channel")] public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("deleted")] public bool Deleted { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("billing_address")] public BillingAddress? BillingAddress { get; set; }

    [JsonPropertyName("card_status")] public string CardStatus { get; set; } = string.Empty;

    [JsonPropertyName("promotional_credits")]
    public int PromotionalCredits { get; set; }

    [JsonPropertyName("refundable_credits")]
    public int RefundableCredits { get; set; }

    [JsonPropertyName("excess_payments")] public int ExcessPayments { get; set; }

    [JsonPropertyName("unbilled_charges")] public int UnbilledCharges { get; set; }

    [JsonPropertyName("preferred_currency_code")]
    public string PreferredCurrencyCode { get; set; } = string.Empty;

    [JsonPropertyName("primary_payment_source_id")]
    public string PrimaryPaymentSourceId { get; set; } = string.Empty;
}

public class Subscription
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("billing_period")] public int BillingPeriod { get; set; }

    [JsonPropertyName("billing_period_unit")]
    public BillingPeriodUnit BillingPeriodUnit { get; set; }

    [JsonPropertyName("po_number")] public string PoNumber { get; set; } = string.Empty;

    [JsonPropertyName("customer_id")] public string CustomerId { get; set; } = string.Empty;

    [JsonPropertyName("status")] public SubscriptionStatus Status { get; set; }

    [JsonPropertyName("current_term_start")] public long CurrentTermStart { get; set; }

    [JsonPropertyName("current_term_end")] public long CurrentTermEnd { get; set; }

    [JsonPropertyName("next_billing_at")] public long NextBillingAt { get; set; }

    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }

    [JsonPropertyName("started_at")] public long StartedAt { get; set; }

    [JsonPropertyName("activated_at")] public long ActivatedAt { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("has_scheduled_changes")]
    public bool HasScheduledChanges { get; set; }

    [JsonPropertyName("channel")] public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("deleted")] public bool Deleted { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("currency_code")] public string CurrencyCode { get; set; } = string.Empty;


    [JsonPropertyName("subscription_items")]
    public List<SubscriptionItem>? SubscriptionItems { get; set; }

    [JsonPropertyName("due_invoices_count")]
    public int DueInvoicesCount { get; set; }

    [JsonPropertyName("mrr")] public int Mrr { get; set; }

    [JsonPropertyName("override_relationship")] public bool OverrideRelationship { get; set; }

    [JsonPropertyName("create_pending_invoices")] public bool CreatePendingInvoices { get; set; }

    [JsonPropertyName("auto_close_invoices")] public bool AutoCloseInvoices { get; set; }

}

public class SubscriptionItem
{
    [JsonPropertyName("item_price_id")] public string ItemPriceId { get; set; } = string.Empty;

    [JsonPropertyName("item_type")] public ItemType Type { get; set; }

    [JsonPropertyName("quantity")] public int Quantity { get; set; }

    [JsonPropertyName("quantity_in_double")] public string QuantityIndouble { get; set; } = string.Empty;

    [JsonPropertyName("unit_price")] public double UnitPrice { get; set; }

    [JsonPropertyName("unit_price_in_double")] public string UnitPriceIndouble { get; set; } = string.Empty;

    [JsonPropertyName("amount")] public double Amount { get; set; }

    [JsonPropertyName("amount_in_double")] public string AmountIndouble { get; set; } = string.Empty;

    [JsonPropertyName("free_quantity")] public int FreeQuantity { get; set; }

    [JsonPropertyName("free_quantity_in_double")] public string FreeQuantityIndouble { get; set; } = string.Empty;

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;
}

public class ItemFamily
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("status")] public ProductFamilyStatus Status { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}

public class Item
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("external_name")] public string ExternalName { get; set; } = string.Empty;

    [JsonPropertyName("item_family_id")] public string ItemFamilyId { get; set; } = string.Empty;

    [JsonPropertyName("status")] public ItemStatus Status { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("type")] public ItemType Type { get; set; }

    [JsonPropertyName("is_shippable")] public bool IsShippable { get; set; }

    [JsonPropertyName("is_giftable")] public bool IsGiftable { get; set; }

    [JsonPropertyName("enabled_for_checkout")]
    public bool EnableForCheckout { get; set; }

    [JsonPropertyName("enabled_in_portal")]
    public bool EnableForPortal { get; set; }

    [JsonPropertyName("metered")] public bool Metered { get; set; }

    [JsonPropertyName("archivable")] public bool Archivable { get; set; }

    [JsonPropertyName("channel")] public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;

    [JsonPropertyName("unit")] public string Unit { get; set; } = string.Empty;

    [JsonPropertyName("usage_calculation")] public string UsageCalculation { get; set; } = string.Empty;

    [JsonPropertyName("redirect_url")] public string RedirectUrl { get; set; } = string.Empty;

    [JsonPropertyName("item_applicability")] public string ItemApplicability { get; set; } = string.Empty;
}

public class ItemPrice
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("item_family_id")] public string ItemFamilyId { get; set; } = string.Empty;

    [JsonPropertyName("item_id")] public string ItemId { get; set; } = string.Empty;

    [JsonPropertyName("status")] public ItemStatus Status { get; set; }

    [JsonPropertyName("external_name")] public string ExternalName { get; set; } = string.Empty;

    [JsonPropertyName("pricing_model")] public string PricingModel { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("archivable")] public bool Archivable { get; set; }

    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }

    [JsonPropertyName("price")] public double Price { get; set; }

    [JsonPropertyName("price_in_double")] public string PricingIndouble { get; set; } = string.Empty;

    [JsonPropertyName("period")] public int Period { get; set; }

    [JsonPropertyName("period_unit")] public BillingPeriodUnit PeriodUnit { get; set; }

    [JsonPropertyName("currency_code")] public string CurrencyCode { get; set; } = string.Empty;

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("free_quantity")] public int FreeQuantity { get; set; }

    [JsonPropertyName("free_quantity_in_double")] public string FreeQuantityIndouble { get; set; } = string.Empty;

    [JsonPropertyName("channel")] public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("is_taxable")] public bool IsTaxable { get; set; }

    [JsonPropertyName("metadata")] public object? Metadata { get; set; }

    [JsonPropertyName("item_type")] public ItemType ItemType { get; set; }

    [JsonPropertyName("show_description_in_invoices")] public bool ShowDescriptionInInvoices { get; set; }

    [JsonPropertyName("show_description_in_quotes")] public bool ShowDescriptionInQuotes { get; set; }
}

public class BillingAddress
{
    [JsonPropertyName("line1")] public string Line1 { get; set; } = string.Empty;
    [JsonPropertyName("line2")] public string Line2 { get; set; } = string.Empty;
    [JsonPropertyName("city")] public string City { get; set; } = string.Empty;
    [JsonPropertyName("state_code")] public string StateCode { get; set; } = string.Empty;
    [JsonPropertyName("state")] public string State { get; set; } = string.Empty;
    [JsonPropertyName("country")] public string Country { get; set; } = string.Empty;
    [JsonPropertyName("zip")] public string PostalCode { get; set; } = string.Empty;
    [JsonPropertyName("validation_status")] public string ValidationStatus { get; set; } = string.Empty;
}

#region Un-used models
public class PaymentMethod
{
    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    [JsonPropertyName("reference_id")] public string ReferenceId { get; set; } = string.Empty;

    [JsonPropertyName("gateway")] public string Gateway { get; set; } = string.Empty;


    [JsonPropertyName("gateway_account_id")]
    public string GatewayAccountId { get; set; } = string.Empty;

    [JsonPropertyName("status")] public string Status { get; set; } = string.Empty;
}
public class Card
{
    [JsonPropertyName("status")] public string Status { get; set; } = string.Empty;

    [JsonPropertyName("gateway")] public string Gateway { get; set; } = string.Empty;

    [JsonPropertyName("gateway_account_id")]
    public string GatewayAccountId { get; set; } = string.Empty;

    [JsonPropertyName("first_name")] public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("last_name")] public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("iin")] public string Iin { get; set; } = string.Empty;

    [JsonPropertyName("last4")] public string Last4 { get; set; } = string.Empty;

    [JsonPropertyName("card_type")] public string CardType { get; set; } = string.Empty;

    [JsonPropertyName("funding_type")] public string FundingType { get; set; } = string.Empty;

    [JsonPropertyName("expiry_month")] public int ExpiryMonth { get; set; }

    [JsonPropertyName("expiry_year")] public int ExpiryYear { get; set; }

    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("masked_number")] public string MaskedNumber { get; set; } = string.Empty;

    [JsonPropertyName("customer_id")] public string CustomerId { get; set; } = string.Empty;

    [JsonPropertyName("payment_source_id")]
    public string PaymentSourceId { get; set; } = string.Empty;
}
public class Plan
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("price")] public double Price { get; set; }

    [JsonPropertyName("period")] public int Period { get; set; }

    [JsonPropertyName("period_unit")] public BillingPeriodUnit PeriodUnit { get; set; }

    [JsonPropertyName("charge_model")] public string ChargeModel { get; set; } = string.Empty;

    [JsonPropertyName("free_quantity")] public int FreeQuantity { get; set; }

    [JsonPropertyName("status")] public PlanStatus Status { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("currency_code")] public string CurrencyCode { get; set; } = string.Empty;

    [JsonPropertyName("enabled_in_hosted_pages")]
    public bool EnabledInHostedPages { get; set; }

    [JsonPropertyName("enabled_in_portal")]
    public bool EnabledInPortal { get; set; }

    [JsonPropertyName("taxable")] public bool Taxable { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}
public class AttachedItem
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("parent_item_id")] public string ParentItemId { get; set; } = string.Empty;

    [JsonPropertyName("item_id")] public string ItemId { get; set; } = string.Empty;

    [JsonPropertyName("status")] public ItemStatus Status { get; set; }

    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("charge_on_event")] public string ChargeOnEvent { get; set; } = string.Empty;

    [JsonPropertyName("charge_once")] public bool ChargeOnce { get; set; }
}
public class AddOn
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    [JsonPropertyName("charge_type")] public string ChargeType { get; set; } = string.Empty;

    [JsonPropertyName("resource_version")] public long ResourceVersion { get; set; }

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("currency_code")] public string CurrencyCode { get; set; } = string.Empty;

    [JsonPropertyName("status")] public SubscriptionStatus Status { get; set; }

    [JsonPropertyName("period_unit")] public BillingPeriodUnit BillingPeriodUnit { get; set; }

    [JsonPropertyName("period")] public int Period { get; set; }

    [JsonPropertyName("price")] public double Price { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("enabled_in_portal")] public bool EnableInPortal { get; set; }

    [JsonPropertyName("taxable")] public bool Taxable { get; set; }
}
#endregion

