﻿namespace BinaryPlate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController : ApiController
{
    #region Private Fields

    private readonly IHubContext<DashboardHub> _dashboardHubContext;
    private readonly TimerManager _timerManager;
    private readonly ITenantResolver _tenantResolver;

    #endregion Private Fields

    #region Public Constructors

    public DashboardController(IHubContext<DashboardHub> dashboardHubContext,
                               TimerManager timerManager,
                               ITenantResolver tenantResolver)
    {
        _dashboardHubContext = dashboardHubContext;
        _timerManager = timerManager;
        _tenantResolver = tenantResolver;
    }

    #endregion Public Constructors

    #region Public Methods

    [HttpPost("GetHeadlinesData")]
    public async Task<IActionResult> GetHeadlinesData()
    {
        var response = await Mediator.Send(new GetHeadlinesQuery());

        _timerManager.PrepareTimer(() => _dashboardHubContext.Clients.All.SendAsync("SendHeadlinesData", response.Payload));

        return TryGetResult(response);
    }

    #endregion Public Methods
}