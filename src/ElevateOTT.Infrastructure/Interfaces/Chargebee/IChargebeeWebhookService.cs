namespace ElevateOTT.Infrastructure.Interfaces.Chargebee;
public interface IChargebeeWebhookService
{
    bool VerifyRequestFromChargebee(string authHeader);
    //Task HandleWebHookEvent(Guid webhookKey, ChargebeeWebHookRequest hookRequest);
}
