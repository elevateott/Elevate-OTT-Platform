using ElevateOTT.Application.Common.Models.Mux;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElevateOTT.WebAPI.Controllers;

[Route("api/mux-webhook")]
[ApiController]
public class MuxWebhookController : ApiController
{
    [HttpPost("callback")]
    public async Task<IActionResult> Post([FromBody] MuxWebhookRequest? hookRequest)
    {
        //string muxHeader = Request.Headers["mux-signature"];
        //(string timestamp, string muxSignature) =
        //    _service.MuxWebHookService.GetMuxTimestampAndSignature(muxHeader);

        //Request.Body.Seek(0, SeekOrigin.Begin);
        //var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();

        //bool requestFromMux =
        //    _service.MuxWebHookService.VerifyRequestFromMux(timestamp, muxSignature, rawRequestBody);

        //if (!requestFromMux) return BadRequest();
        //var eventHandled = await _service.MuxWebHookService.HandleWebHookEvent(hookRequest);
        //if (!eventHandled) return BadRequest();

        return Ok();
    }
}
