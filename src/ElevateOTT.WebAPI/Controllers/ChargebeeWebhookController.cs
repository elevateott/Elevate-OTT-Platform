using ElevateOTT.Application.Common.Models.Chargebee;

namespace ElevateOTT.WebAPI.Controllers;

[Route("api/chragebee-webhook")]
[ApiController]
public class ChargebeeWebhookController : ApiController
{
    /// <summary>
    /// Webhook for Chargebee integration.
    /// </summary>
    /// <param name="hookRequest"></param>
    /// <returns></returns>
    [HttpPost("callback/{webhookKey:guid}")]
    public async Task<IActionResult> Callback(Guid webhookKey, [FromBody] ChargebeeWebHookRequest? hookRequest)
    {
        // TODO create different service for tenant callback

        // TODO set tenant id in tenant resolver before calls to db

        //if (hookRequest == null) return BadRequest();

        //string authHeader = Request.Headers["authorization"];
        //bool isFromChargebee = _service.ChargebeeWebhookService.VerifyRequestFromChargebee(authHeader);

        //Request.Body.Seek(0, SeekOrigin.Begin);
        //var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();


        //if (isFromChargebee)
        //{
        //    await _service.ChargebeeWebhookService.HandleWebHookEvent(webhookKey, hookRequest);
        //}

        return Ok();
    }
}
