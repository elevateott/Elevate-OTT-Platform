namespace BinaryPlate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[BpAuthorize]
public class ApplicantsController : ApiController
{
    #region Public Methods

    [HttpPost("GetApplicant")]
    public async Task<IActionResult> GetApplicant(GetApplicantForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetApplicants")]
    public async Task<IActionResult> GetApplicants(GetApplicantsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("CreateApplicant")]
    public async Task<IActionResult> CreateApplicant(CreateApplicantCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPut("UpdateApplicant")]
    public async Task<IActionResult> UpdateApplicant(UpdateApplicantCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete("DeleteApplicant")]
    public async Task<IActionResult> DeleteApplicant(string id)
    {
        var response = await Mediator.Send(new DeleteApplicantCommand { Id = id });
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("GetApplicantNoAuth")]
    public async Task<IActionResult> GetApplicantNoAuth(GetApplicantForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("GetApplicantsNoAuth")]
    public async Task<IActionResult> GetApplicantsNoAuth(GetApplicantsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("CreateApplicantNoAuth")]
    public async Task<IActionResult> CreateApplicantNoAuth(CreateApplicantCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPut("UpdateApplicantNoAuth")]
    public async Task<IActionResult> UpdateApplicantNoAuth(UpdateApplicantCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpDelete("DeleteApplicantNoAuth")]
    public async Task<IActionResult> DeleteApplicantNoAuth(string id)
    {
        var response = await Mediator.Send(new DeleteApplicantCommand { Id = id });
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("FluentValidation")]
    public async Task<IActionResult> FluentValidation(CreateApplicantCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetApplicantReferences")]
    public async Task<IActionResult> GetApplicantReferences(GetApplicantReferencesQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("ExportAsXls")]
    public async Task<IActionResult> ExportAsXls(ExportApplicantsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [AllowAnonymous]
    [HttpPost("ExportAsPdf")]
    public async Task<IActionResult> ExportAsPdf(ExportApplicantsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    #endregion Public Methods
}