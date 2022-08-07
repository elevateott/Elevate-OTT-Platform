using ElevateOTT.Application.Common.Interfaces.Mux;
using ElevateOTT.Application.Common.Models.Mux;

namespace ElevateOTT.WebAPI.Controllers;

[Route("api/mux-webhook")]
[ApiController]
public class MuxWebhookController : ApiController
{
    private readonly IMuxWebhookService? _webhookService;

    public MuxWebhookController(IMuxWebhookService? webhookService)
    {
        _webhookService = webhookService;
    }

    [HttpPost("callback")]
    public async Task<IActionResult> Post([FromBody] MuxWebhookRequest? hookRequest)
    {
        string muxHeader = Request.Headers["mux-signature"];
        (string timestamp, string muxSignature) =
            _webhookService.GetMuxTimestampAndSignature(muxHeader);

        Request.Body.Seek(0, SeekOrigin.Begin);
        var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();

        bool requestFromMux =
            _webhookService.VerifyRequestFromMux(timestamp, muxSignature, rawRequestBody);

        if (!requestFromMux) return BadRequest();
        var eventHandled = await _webhookService.HandleWebHookEvent(hookRequest);
        if (!eventHandled) return BadRequest();

        return Ok();
    }
}
