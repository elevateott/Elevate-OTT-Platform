using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using ElevateOTT.Infrastructure.Interfaces.Chargebee;
using ElevateOTT.Infrastructure.Models.Chargebee;
using ProductFamilyStatus = ElevateOTT.Infrastructure.Enums.ProductFamilyStatus;


namespace ElevateOTT.Infrastructure.Services.Chargebee;

public class ChargebeeWebhookService : IChargebeeWebhookService
{
    //private readonly IRepositoryManager _repository;
    //private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    private readonly Guid _webHookKey;

    public ChargebeeWebhookService(
        IMapper mapper,
        IConfiguration configuration)
    {
        //_repository = repository;
        //_logger = logger;
        _mapper = mapper;
        _configuration = configuration;

        // This webhook key is assigned to Elevate OTT.
        // Elevate OTT tenants may have their own webhook key.
        _webHookKey = Guid.Parse(configuration["Chargebee:WebHookKey"]);
    }

    public async Task HandleWebHookEvent(Guid webhookKey, ChargebeeWebHookRequest hookRequest)
    {
        // TODO handle duplicates
        // ref: https://apidocs.chargebee.com/docs/api/events?prod_cat_ver=2
        // TODO check if event id is persisted
        // If so then skip
        // save event after handled
        // repo for webhook events (no service needed)
        // delete event if 4 days old


        // A null tenantId indicates webhook sent from Elevate OTT Chargebee account
        Guid? tenantId = null;

        // If web hook key assigned to a tenant
        if (!webhookKey.Equals(_webHookKey))
        {
            tenantId = await GetTenantIdByWebHookKey(webhookKey);
            if (tenantId == null) return;
        }

        bool eventHandled = false;

        switch (hookRequest.EventType)
        {
            case "customer_created":
                eventHandled = await HandleCustomerCreated(tenantId, hookRequest);
                break;
            case "customer_changed":
                eventHandled = await HandleCustomerChanged(tenantId, hookRequest);
                break;
            case "customer_deleted":
                eventHandled = await HandleCustomerDeleted(tenantId, hookRequest);
                break;
            case "customer_moved_out":
                eventHandled = await HandleCustomerMovedOut(tenantId, hookRequest);
                break;
            case "customer_moved_in":
                eventHandled = await HandleCustomerMovedIn(tenantId, hookRequest);
                break;
            case "subscription_created":
                eventHandled = await HandleSubscriptionCreated(tenantId, hookRequest);
                break;
            case "subscription_started":
                eventHandled = await HandleSubscriptionStarted(tenantId, hookRequest);
                break;
            case "subscription_activated":
                eventHandled = await HandleSubscriptionActivated(tenantId, hookRequest);
                break;
            case "subscription_changed":
                eventHandled = await HandleSubscriptionChanged(tenantId, hookRequest);
                break;
            case "subscription_cancelled":
                eventHandled = await HandleSubscriptionCancelled(tenantId, hookRequest);
                break;
            case "subscription_reactivated":
                eventHandled = await HandleSubscriptionReactivated(tenantId, hookRequest);
                break;
            case "subscription_renewed":
                eventHandled = await HandleSubscriptionRenewed(tenantId, hookRequest);
                break;
            case "subscription_deleted":
                eventHandled = await HandleSubscriptionDeleted(tenantId, hookRequest);
                break;
            case "subscription_paused":
                eventHandled = await HandleSubscriptionPaused(tenantId, hookRequest);
                break;
            case "subscription_resumed":
                eventHandled = await HandleSubscriptionResumed(tenantId, hookRequest);
                break;
            case "item_family_created":
                eventHandled = await HandleItemFamilyCreated(tenantId, hookRequest);
                break;
            case "item_family_updated":
                eventHandled = await HandleItemFamilyUpdated(tenantId, hookRequest);
                break;
            case "item_family_deleted":
                eventHandled = await HandleItemFamilyDeleted(tenantId, hookRequest);
                break;
            case "item_created":
                eventHandled = await HandleItemCreated(tenantId, hookRequest);
                break;
            case "item_updated":
                eventHandled = await HandleItemUpdated(tenantId, hookRequest);
                break;
            case "item_deleted":
                eventHandled = await HandleItemDeleted(tenantId, hookRequest);
                break;
            case "item_price_created":
                eventHandled = await HandleItemPriceCreated(tenantId, hookRequest);
                break;
            case "item_price_updated":
                eventHandled = await HandleItemPriceUpdated(tenantId, hookRequest);
                break;
            case "item_price_deleted":
                eventHandled = await HandleItemPriceDeleted(tenantId, hookRequest);
                break;
            default:
                _logger.LogInfo("HandleWebHookEvent invoked but to type found.");
                return;
        }

        // TODO save event to db if handled

        return;
    }

    #region Customer Handlers
    private async Task<bool> HandleCustomerCreated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        // TODO use guard nuget for all guard clauses 
        // ref: https://www.nuget.org/packages/Ardalis.GuardClauses/
        // ref: https://github.com/ardalis/guardclauses
        if (hookRequest.Content?.Customer == null) return false;

        // hookRequest = Guard.Against.Null(hookRequest, nameof(hookRequest))

        var customer = hookRequest.Content.Customer;

        //if (tenantId == null)
        //{
        //    await UpdateTenant(customer);
        //}

        // TODO handle Elevate OTT tenants' OTT customers created


        return true;
    }
    private async Task<bool> HandleCustomerChanged(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Customer == null) return false;

        var customer = hookRequest.Content.Customer;

        //if (tenantId == null)
        //{
        //    await UpdateTenant(customer);

        //}
        //else // if customer of Elevate OTT tenant
        //{
        //    await UpdateCustomerForTenant(tenantId.Value, customer);
        //}

        return true;
    }

    private async Task<bool> HandleCustomerDeleted(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Customer == null) return false;

        var customer = hookRequest.Content.Customer;

        var tenant =
            await _repository.Tenant.FindTenantByConditionAsync(s =>
                s.ChargebeeId.Equals(customer.Id), trackChanges: true);

        if (tenant != null)
        {
            if (tenant.ResourceVersion >= customer.ResourceVersion) return false;

            tenant.Deleted = true;
        }
        else // if customer of Elevate OTT tenant
        {
        }

        return true;
    }

    private async Task<bool> HandleCustomerMovedIn(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> HandleCustomerMovedOut(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region ProductItem Family Handlers
    private async Task<bool> HandleItemFamilyCreated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.ItemFamily == null) return false;

        var itemFamily = hookRequest.Content.ItemFamily;

        bool itemFamilyExists =
            await _repository.ProductFamily.ProductFamilyExistsAsync(f =>
                f.ChargebeeItemFamilyId.Equals(itemFamily.Id) && f.TenantId.Equals(tenantId));

        if (itemFamilyExists) return false;


        // TODO use mediatR

        //var newProductFamily = new ProductFamilyModel
        //{
        //    ChargebeeItemFamilyId = itemFamily.Id,
        //    Name = itemFamily.Name,
        //    Description = itemFamily.Description,
        //    Status = (ProductFamilyStatus)itemFamily.Status,
        //    ResourceVersion = itemFamily.ResourceVersion,
        //    Object = itemFamily.Object
        //};

        //_repository.ProductFamily.CreateProductFamily(tenantId, newProductFamily);
        //await _repository.SaveAsync();

        return true;
    }
    private async Task<bool> HandleItemFamilyUpdated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.ItemFamily == null) return false;

        var itemFamily = hookRequest.Content.ItemFamily;

        var itemFamilyToUpdate = await _repository.ProductFamily.FindProductFamilyByConditionAsync(f =>
            f.ChargebeeItemFamilyId.Equals(itemFamily.Id) && f.TenantId.Equals(tenantId), trackChanges: true);

        if (itemFamilyToUpdate == null
            || itemFamilyToUpdate.ResourceVersion >= itemFamily.ResourceVersion) return false;

        itemFamilyToUpdate.ChargebeeItemFamilyId = itemFamily.Id;
        itemFamilyToUpdate.Name = itemFamily.Name;
        itemFamilyToUpdate.Description = itemFamily.Description;
        itemFamilyToUpdate.Status = (ProductFamilyStatus)itemFamily.Status;
        itemFamilyToUpdate.ResourceVersion = itemFamily.ResourceVersion;
        itemFamilyToUpdate.Object = itemFamily.Object;

        await _repository.SaveAsync();

        return true;
    }
    private async Task<bool> HandleItemFamilyDeleted(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.ItemFamily == null) return false;

        var itemFamily = hookRequest.Content.ItemFamily;

        var itemFamilyToDelete = await _repository.ProductFamily.FindProductFamilyByConditionAsync(f =>
                f.ChargebeeItemFamilyId.Equals(itemFamily.Id) && f.TenantId.Equals(tenantId),
            trackChanges: false);

        if (itemFamilyToDelete == null
            || itemFamilyToDelete.ResourceVersion >= itemFamily.ResourceVersion) return false;

        if (itemFamily.Status == ProductFamilyStatus.Deleted)
        {
            _repository.ProductFamily.DeleteProductFamily(itemFamilyToDelete);
            await _repository.SaveAsync();
        }

        return true;
    }
    #endregion

    #region Subscription Handlers
    private async Task<bool> HandleSubscriptionCreated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;
        if (hookRequest.Content?.Customer == null) return false;

        var subscription = hookRequest.Content.Subscription;

        var tenant =
            await _repository.Tenant.FindTenantByConditionAsync(s =>
                s.ChargebeeId.Equals(hookRequest.Content.Customer.Id), trackChanges: true);

        if (tenant == null) return false;

        bool subscriptionExists =
            await _repository.Subscription.SubscriptionExistsAsync(s =>
                s.ChargebeeSubscriptionId.Equals(subscription.Id));

        if (subscriptionExists) return true;

        // TODO use mediatR

        //var newSubscription = _mapper.Map<SubscriptionForCreationDto>(subscription);
        //newSubscription.ChargebeeSubscriptionId = subscription.Id;
        //newSubscription.Status = (SubscriptionStatus)subscription.Status;
        //newSubscription.BillingPeriodUnit = (BillingPeriodUnit)subscription.BillingPeriodUnit;
        //await _service.SubscriptionService.CreateSubscriptionForTenantAsync(tenant.Id, newSubscription,
        //    false);

        // TODO items, addons


        return true;
    }

    private async Task<bool> HandleSubscriptionRenewed(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        await _repository.SaveAsync();

        return true;
    }

    private async Task<bool> HandleSubscriptionReactivated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        subscriptionToUpdate.Deleted = subscription.Deleted;

        await _repository.SaveAsync();

        return true;
    }

    private async Task<bool> HandleSubscriptionCancelled(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        subscriptionToUpdate.Deleted = subscription.Deleted;

        await _repository.SaveAsync();

        return true;
    }
    private async Task<bool> HandleSubscriptionChanged(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        subscriptionToUpdate.Deleted = subscription.Deleted;

        await _repository.SaveAsync();

        return true;
    }

    private async Task<bool> HandleSubscriptionActivated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        subscriptionToUpdate.Deleted = subscription.Deleted;

        await _repository.SaveAsync();

        return true;
    }

    private async Task<bool> HandleSubscriptionStarted(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        subscriptionToUpdate.Deleted = subscription.Deleted;

        await _repository.SaveAsync();

        return true;
    }

    private async Task<bool> HandleSubscriptionDeleted(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Deleted = subscription.Deleted;
        await _repository.SaveAsync();

        return true;
    }

    private async Task<bool> HandleSubscriptionPaused(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        subscriptionToUpdate.Deleted = subscription.Deleted;

        await _repository.SaveAsync();

        return true;
    }

    private async Task<bool> HandleSubscriptionResumed(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Subscription == null) return false;

        var subscription = hookRequest.Content?.Subscription;

        var subscriptionToUpdate =
            await _repository.Subscription.FindSubscriptionByConditionAsync(
                s => s.ChargebeeSubscriptionId.Equals(subscription.Id), true);

        if (subscriptionToUpdate == null
            || subscriptionToUpdate.ResourceVersion >= subscription.ResourceVersion) return false;

        subscriptionToUpdate.Status = (SubscriptionStatus)subscription.Status;
        subscriptionToUpdate.Deleted = subscription.Deleted;

        await _repository.SaveAsync();

        return true;
    }
    #endregion

    #region Product Item Handlers
    private async Task<bool> HandleItemCreated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Item == null) return false;

        var item = hookRequest.Content.Item;

        var productFamily =
            await _repository.ProductFamily.FindProductFamilyByConditionAsync(f =>
                    f.ChargebeeItemFamilyId.Equals(item.ItemFamilyId) && f.TenantId.Equals(tenantId),
                trackChanges: false);

        if (productFamily == null) return false;

        bool itemExists =
            await _repository.ProductItem.ProductItemExistsAsync(p =>
                p.ItemId.Equals(item.Id) && p.ProductFamilyId.Equals(productFamily.Id));

        if (itemExists) return false;

        var newProductItem = new ProductItemModel()
        {
            ItemId = item.Id,
            Name = item.Name,
            Status = (ItemStatus)item.Status,
            ResourceVersion = item.ResourceVersion,
            Object = item.Object,
            Description = item.Description,
            ExternalName = item.ExternalName,
            ItemFamilyId = item.ItemFamilyId,
            Type = (ProductItemType)item.Type,
            IsShippable = item.IsShippable,
            IsGiftable = item.IsGiftable,
            EnableForCheckout = item.EnableForCheckout,
            EnabledInPortal = item.EnableForPortal,
            Metered = item.Metered,
            Archivable = item.Archivable,
            Channel = item.Channel,
            Unit = item.Unit,
            UsageCalculation = item.UsageCalculation,
            RedirectUrl = item.RedirectUrl,
            ItemApplicability = item.ItemApplicability,
        };

        _repository.ProductItem.CreateProductItemForProductFamily(productFamily.Id,
            newProductItem);
        await _repository.SaveAsync();

        return true;
    }
    private async Task<bool> HandleItemUpdated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Item == null) return false;

        var item = hookRequest.Content.Item;

        var productFamily =
            await _repository.ProductFamily.FindProductFamilyByConditionAsync(f =>
                    f.ChargebeeItemFamilyId.Equals(item.ItemFamilyId) && f.TenantId.Equals(tenantId),
                trackChanges: false);

        if (productFamily == null) return false;

        var productItemToUpdate =
            await _repository.ProductItem.FindItemByConditionAsync(i =>
                i.ItemId.Equals(hookRequest.Id) && i.ProductFamilyId.Equals(productFamily.Id), trackChanges: true);

        if (productItemToUpdate == null
            || productItemToUpdate.ResourceVersion >= item.ResourceVersion) return false;

        productItemToUpdate.ItemId = item.Id;
        productItemToUpdate.Name = item.Name;
        productItemToUpdate.Status = (ItemStatus)item.Status;
        productItemToUpdate.ResourceVersion = item.ResourceVersion;
        productItemToUpdate.Object = item.Object;
        productItemToUpdate.Description = item.Description;
        productItemToUpdate.ExternalName = item.ExternalName;
        productItemToUpdate.ItemFamilyId = item.ItemFamilyId;
        productItemToUpdate.Type = (ProductItemType)item.Type;
        productItemToUpdate.IsShippable = item.IsShippable;
        productItemToUpdate.IsGiftable = item.IsGiftable;
        productItemToUpdate.EnableForCheckout = item.EnableForCheckout;
        productItemToUpdate.EnabledInPortal = item.EnableForPortal;
        productItemToUpdate.Metered = item.Metered;
        productItemToUpdate.Archivable = item.Archivable;
        productItemToUpdate.Channel = item.Channel;
        productItemToUpdate.Unit = item.Unit;
        productItemToUpdate.UsageCalculation = item.UsageCalculation;
        productItemToUpdate.RedirectUrl = item.RedirectUrl;
        productItemToUpdate.ItemApplicability = item.ItemApplicability;

        await _repository.SaveAsync();

        return false;
    }
    private async Task<bool> HandleItemDeleted(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        if (hookRequest.Content?.Item == null) return false;

        var item = hookRequest.Content.Item;

        var productFamily =
            await _repository.ProductFamily.FindProductFamilyByConditionAsync(f =>
                    f.ChargebeeItemFamilyId.Equals(item.ItemFamilyId) && f.TenantId.Equals(tenantId),
                trackChanges: false);

        if (productFamily == null) return false;

        var itemToDelete = await _repository.ProductItem.FindItemByConditionAsync(f =>
                f.ItemId.Equals(item.Id) && f.ProductFamilyId.Equals(productFamily.Id),
            trackChanges: false);

        if (itemToDelete == null
            || itemToDelete.ResourceVersion >= item.ResourceVersion) return false;

        if (item.Status == Entities.ChargebeeEnums.ItemStatus.Deleted)
        {
            _repository.ProductItem.DeleteItem(itemToDelete);
            await _repository.SaveAsync();
        }

        return true;
    }
    #endregion

    #region Product Item Price Handlers
    private async Task<bool> HandleItemPriceDeleted(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> HandleItemPriceUpdated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> HandleItemPriceCreated(Guid? tenantId, ChargebeeWebHookRequest hookRequest)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Auth
    public bool VerifyRequestFromChargebee(string authHeader)
    {
        // ref: https://docs.mux.com/guides/video/verify-webhook-signatures

        string encodedCreds = authHeader.Substring(6);

        string username = _configuration["Chargebee:WebHookUserName"];
        string password = _configuration["Chargebee:WebHookPassword"];

        string credentials = $"{username}:{password}";
        var base64String = _service.ServiceUtils.EncodeTo64(credentials);


        // TODO if not match with app's hash, check if db contains same hash
        // tenants will each have their own hash

        return encodedCreds.Equals(base64String);
    }

    public async Task<bool> VerifyRequestFromChargebeeForTenant(string authHeader)
    {
        // ref: https://docs.mux.com/guides/video/verify-webhook-signatures

        string encodedCreds = authHeader.Substring(6);

        // TODO endcode creds encrypted in db

        string encryptedEncodedCreds = _service.CryptoService.EncryptText(encodedCreds);

        return await _repository.Tenant.TenantExistsAsync(s
            => s.ChargebeeWebhookEncodedAuth.Equals(encryptedEncodedCreds));
    }
    #endregion

    #region Helper Methods
    private async Task UpdateTenant(Entities.Models.Chargebee.Customer customer)
    {
        var tenant =
            await _repository.Tenant.FindTenantByConditionAsync(s =>
                s.ChargebeeId.Equals(customer.Id), trackChanges: true);

        if (tenant == null) return;

        if (tenant.ResourceVersion >= customer.ResourceVersion) return;

        tenant.Email = customer.Email;
        tenant.FirstName = customer.FirstName;
        tenant.LastName = customer.LastName;
        tenant.Locale = customer.Locale;
        tenant.Channel = customer.Channel;
        tenant.Company = customer.Company;
        tenant.Phone = customer.Phone;
        tenant.ResourceVersion = customer.ResourceVersion;
        tenant.PiiCleared = customer.PiiCleared;
        tenant.Line1 = customer.BillingAddress?.Line1;
        tenant.Line2 = customer.BillingAddress?.Line2;
        tenant.City = customer.BillingAddress?.City;
        tenant.State = customer.BillingAddress?.State;
        tenant.StateCode = customer.BillingAddress?.StateCode;
        tenant.Country = customer.BillingAddress?.Country;
        tenant.CountryCode = customer.BillingAddress?.Country;
        tenant.PostalCode = customer.BillingAddress?.PostalCode;
        tenant.ValidationStatus = customer.BillingAddress?.ValidationStatus;

        await _repository.SaveAsync();
    }

    private async Task UpdateCustomerForTenant(Guid tenantId, Entities.Models.Chargebee.Customer customer)
    {
        var customerEntity =
            await _repository.OttCustomer
                .FindOttCustomerByConditionAsync(c => c.ChargebeeId.Equals(customer.Id)
                                                      && c.TenantId.Equals(tenantId), trackChanges: true);
        if (customerEntity != null)
        {
            if (customerEntity.ResourceVersion >= customer.ResourceVersion) return;

            customerEntity.Email = customer.Email;
            customerEntity.FirstName = customer.FirstName;
            customerEntity.LastName = customer.LastName;
            customerEntity.Locale = customer.Locale;
            customerEntity.Company = customer.Company;
            customerEntity.Phone = customer.Phone;
            customerEntity.ResourceVersion = customer.ResourceVersion;

            await _repository.SaveAsync();
        }
        else
        {
            // TODO handle scenario where tenant adds customer in their Chargebee portal

            //var newCustomerForTenant = new OttCustomerModel
            //{
            //    TenantId = tenantId,
            //    Email = customer.Email,
            //    FirstName = customer.FirstName,
            //    LastName = customer.LastName,
            //    Locale = customer.Locale,
            //    Company = customer.Company,
            //    Phone = customer.Phone,
            //    ResourceVersion = customer.ResourceVersion,
            //};

            //_repository.CustomerOfTenantId.CreateOttCustomerForTenant(tenantId, newCustomerForTenant);
            //await _repository.SaveAsync();
        }
    }
    private async Task<Guid?> GetTenantIdByWebHookKey(Guid webhookKey)
    {
        var tenant = await _repository.Tenant
            .FindTenantByConditionAsync(s => s.ChargebeeWebhookKey.Equals(webhookKey), trackChanges: false);
        return tenant?.Id;
    }
    #endregion
}
