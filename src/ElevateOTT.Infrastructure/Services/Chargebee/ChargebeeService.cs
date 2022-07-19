using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Infrastructure.Interfaces.Chargebee;
using ElevateOTT.Infrastructure.Models.Chargebee;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace ElevateOTT.Infrastructure.Services.Chargebee;
public class ChargebeeService : IChargebeeService
{
//    private readonly IConfiguration _configuration;
//    private readonly ILoggerManager _logger;
//    private readonly IRepositoryManager _repository;
//    private readonly IServiceManager _service;
//    private readonly string _chargebeeSiteName;
//    private readonly string _chargebeeApiKey;
//    private static Random _randomizer = new Random();

//    public ChargebeeService(IConfiguration configuration,
//        ILoggerManager logger,
//        IRepositoryManager repository,
//        IServiceManager service)
//        : base(repository)
//    {
//        _logger = logger;
//        _configuration = configuration;
//        _repository = repository;
//        _service = service;
//        _chargebeeSiteName = configuration["Chargebee:SiteName"];
//        _chargebeeApiKey = configuration["Chargebee:ApiKey"];
//    }


//    public HostedPageDto GetHostedPageForFreeTrialSubscription(string chargebeeCustomerId)
//    {
//        // ref: https://apidocs.chargebee.com/docs/api/hosted_pages#create_checkout_for_a_new_subscription

//        ApiConfig.Configure(_chargebeeSiteName, _chargebeeApiKey);

//        string subscriptionPlanId = _configuration["Chargebee:FreeTrialPlanId"];
//        string freeTrialUSDMonthlyId = _configuration["Chargebee:FreeTrialUSDMonthlyId"];
//        string freeTrialRedirectUrl = _configuration["Chargebee:FreeTrialRedirectUrl"];
//        string freeTrialCancelUrl = _configuration["Chargebee:FreeTrialCancelUrl"];

//        EntityResult result = HostedPage.CheckoutNewForItems()
//            .SubscriptionCoupon("")
//            .SubscriptionItemItemPriceId(0, freeTrialUSDMonthlyId)
//            .SubscriptionItemQuantity(0, 1)
//            .CustomerId(chargebeeCustomerId)
//            .PassThruContent(Guid.NewGuid().ToString())
//            .RedirectUrl(freeTrialRedirectUrl)
//            .CancelUrl(freeTrialCancelUrl)
//            .Request();

//        HostedPage hostedPage = result.HostedPage;
//        return MapToHostedPageDto(hostedPage);
//    }

//    public JToken GetHostedPageJToken()
//    {
//// ref: https://www.chargebee.com/checkout-portal-docs/api-checkout.html#integration-steps

//        ApiConfig.Configure(_chargebeeSiteName, _chargebeeApiKey);

//        EntityResult result = HostedPage.CheckoutNewForItems()
//            .CustomerEmail("john@user.com")
//            .CustomerFirstName("John")
//            .CustomerLastName("Doe")
//            .CustomerLocale("fr-CA")
//            .CustomerPhone("+1-949-999-9999")
//            .SubscriptionItemItemPriceId(1, "new-plan")
//            .BillingAddressFirstName("John")
//            .BillingAddressLastName("Doe")
//            .BillingAddressLine1("PO Box 9999")
//            .BillingAddressCity("Walnut")
//            .BillingAddressState("California")
//            .BillingAddressZip("91789")
//            .BillingAddressCountry("US").Request();

//        HostedPage hostedPage = result.HostedPage;
//        return hostedPage.GetJToken();
//    }

//    public JToken GetPortalSession()
//    {
//        throw new NotImplementedException();
//    }

//    public string GenerateWebHookKey()
//    {
//        return Guid.NewGuid().ToString().Replace("-", "");
//    }

//    public string GenerateWebHookPassword()
//    {
//        return GenerateAlphanumericString(16);
//    }

//    public string GetWebHookEncodedAuth(string password)
//    {
//        string username = _configuration["Chargebee:WebHookUserName"];
//        string credentials = $"{username}:{password}";
//        return _service.ServiceUtils.EncodeTo64(credentials);
//    }

//    public JToken GetPortalSession(string customerId)
//{
//        ApiConfig.Configure(_chargebeeSiteName, _chargebeeApiKey);
//        EntityResult result = PortalSession.Create()
//            .CustomerId(customerId).Request();
//        PortalSession portalSession = result.PortalSession;
//        JToken jToken = portalSession.GetJToken();

//        return jToken;
//    }

//    public async Task<Customer?> CreateCustomer(Guid appUserId, ChargebeeCustomerForCreationDto customerForCreation)
//{
//        ApiConfig.Configure(_chargebeeSiteName, _chargebeeApiKey);

//        EntityResult result = Customer.Create()
//            .Id(customerForCreation.ChargebeeId)
//               .FirstName(customerForCreation.FirstName)
//               .LastName(customerForCreation.LastName)
//               .Email(customerForCreation.Email)
//               .Phone(customerForCreation.Phone)
//               .Request();

//        Customer customer = result.Customer;

//        return customer;
//    }

//    public void SyncSubscriptionPlanData()
//    {

//    }

//    public async Task<string> GenerateCustomerId()
//    {
//        bool tenantExists = false;
//        bool ottCustomerExists = false;

//        string newId;
//        do
//        {
//            newId = GenerateRandomNumberString(10);

//            tenantExists =
//                await _repository.Tenant
//                    .TenantExistsAsync(s => s.ChargebeeId != null && s.ChargebeeId.Equals(newId));

//            if (!tenantExists)
//                ottCustomerExists =
//                    await _repository.OttCustomer
//                        .OttCustomerExistsAsync(c => c.ChargebeeId.Equals(newId));
//        } while (tenantExists || ottCustomerExists);

//        return newId;
//    }

//    #region private methods
//    private string GenerateAlphanumericString(int length)
//    {
//        const string characters = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
//        return new string(Enumerable.Repeat(characters, length)
//            .Select(s => s[_randomizer.Next(s.Length)]).ToArray());
//    }

//    private string GenerateRandomNumberString(int length)
//    {
//        const string characters = "0123456789";
//        return new string(Enumerable.Repeat(characters, length)
//            .Select(s => s[_randomizer.Next(s.Length)]).ToArray());
//    }

//    private string ConvertHostedPageStateToString(HostedPage.StateEnum? state)
//    {
//        return state switch
//        {
//            HostedPage.StateEnum.UnKnown => "unknown",
//            HostedPage.StateEnum.Created => "created",
//            HostedPage.StateEnum.Requested => "requested",
//            HostedPage.StateEnum.Succeeded => "succeeded",
//            HostedPage.StateEnum.Cancelled => "cancelled",
//            HostedPage.StateEnum.Acknowledged => "acknowledged",
//            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
//        };
//    }
//    private string ConvertHostedPageTypeToString(HostedPage.TypeEnum? type)
//    {
//        return type switch
//        {
//            HostedPage.TypeEnum.CheckoutNew => "checkout_new",
//            HostedPage.TypeEnum.CheckoutExisting => "checkout_existing",
//            HostedPage.TypeEnum.CheckoutOneTime => "checkout_one_time",
//            HostedPage.TypeEnum.CheckoutGift => "checkout_gift",
//            HostedPage.TypeEnum.UpdatePaymentMethod => "update_payment_methods",
//            HostedPage.TypeEnum.ClaimGift => "claim_gift",
//            HostedPage.TypeEnum.ExtendSubscription => "extend_subscription",
//            HostedPage.TypeEnum.ManagePaymentSources => "manage_payment_sources",
//            HostedPage.TypeEnum.CollectNow => "collect_now",
//            HostedPage.TypeEnum.UnKnown => "unknown",
//            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
//        };
//    }

//    private HostedPageDto MapToHostedPageDto(HostedPage hostedPage)
//    {
//        return new HostedPageDto
//        {
//            Id = hostedPage.Id,
//            Url = hostedPage.Url,
//            State = ConvertHostedPageStateToString(hostedPage.State),
//            CreatedAt = hostedPage.CreatedAt != null ? _service.ServiceUtils.DateTimeToUnix((DateTime)hostedPage.CreatedAt) : 0,
//            ExpiresAt = hostedPage.ExpiresAt != null ? _service.ServiceUtils.DateTimeToUnix((DateTime)hostedPage.ExpiresAt) : 0,
//            UpdatedAt = hostedPage.UpdatedAt != null ? _service.ServiceUtils.DateTimeToUnix((DateTime)hostedPage.UpdatedAt) : 0,
//            HostedPageType = ConvertHostedPageTypeToString(hostedPage.HostedPageType),
//            Embed = hostedPage.Embed,
//            PassThruContent = hostedPage.PassThruContent,
//            ResourceVersion = hostedPage.ResourceVersion ?? 0
//        };

//        // TODO should I pass this?????
//        // hostedPage.GetJToken();
//    }
//    #endregion
}
