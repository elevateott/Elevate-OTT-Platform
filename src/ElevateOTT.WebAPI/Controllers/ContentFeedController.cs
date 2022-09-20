using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElevateOTT.WebAPI.Controllers;

//[BpAuthorize]
[AllowAnonymous]
[Route("api/contentfeed")]
[ApiController]
public class ContentFeedController : ApiController
{
    #region Public Methods

    // GET content feed URL for tenant
    // POST save content feed for tenant
    [HttpPost]
    public async Task<IActionResult> Post()
    {

        return Ok();
    }

    #endregion
}
