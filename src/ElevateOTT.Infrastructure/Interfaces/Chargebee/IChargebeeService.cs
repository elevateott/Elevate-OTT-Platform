using Newtonsoft.Json.Linq;
// using Customer = ChargeBee.Models.Customer;

namespace ElevateOTT.Infrastructure.Interfaces.Chargebee;

public interface IChargebeeService
{
    //Task<Customer?> CreateCustomer(Guid appUserId, ChargebeeCustomerForCreationDto customerForCreation);

    //HostedPageDto GetHostedPageForFreeTrialSubscription(string tenantId);

    JToken GetHostedPageJToken();

    JToken GetPortalSession();

    string GenerateWebHookKey();

    string GenerateWebHookPassword();

    string GetWebHookEncodedAuth(string password);

    Task<string> GenerateCustomerId();

    // create subscription

    // create add-on
    // Retrieve a subscription
    // Retrieve a subscriptions
    // Retrieve a customer
    // Retrieve a customer
    // metered items??
    // 
}

