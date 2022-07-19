using ElevateOTT.Infrastructure.Models.Mux;

namespace ElevateOTT.Infrastructure.Interfaces.Mux;

public interface IMuxWebhookService
{
    Task<bool> HandleWebHookEvent(MuxWebhookRequest hookRequest);
    (string timestamp, string muxSignature) GetMuxTimestampAndSignature(string muxHeader);
    bool VerifyRequestFromMux(string timestamp, string signature, string requestBody);
    Task TestSignalR();
}
