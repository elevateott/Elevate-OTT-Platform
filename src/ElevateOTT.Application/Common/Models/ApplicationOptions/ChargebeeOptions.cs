using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Common.Models.ApplicationOptions;
public class ChargebeeOptions
{
    #region Public Fields

    public const string Section = "Chargebee";

    #endregion Public Fields

    #region Public Properties

    public string SiteName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string WebHookUserName { get; set; } = string.Empty;
    public string WebHookPassword { get; set; } = string.Empty;
    public string WebHookKey { get; set; } = string.Empty;
    public string FreeTrialPlanId { get; set; } = string.Empty;
    public string Tier1PlanId { get; set; } = string.Empty;
    public string Tier2PlanId { get; set; } = string.Empty;
    public string FreeTrialUSDMonthlyId { get; set; } = string.Empty;
    public string FreeTrialRedirectUrl { get; set; } = string.Empty;
    public string FreeTrialCancelUrl { get; set; } = string.Empty;
    
    #endregion Public Properties
}
