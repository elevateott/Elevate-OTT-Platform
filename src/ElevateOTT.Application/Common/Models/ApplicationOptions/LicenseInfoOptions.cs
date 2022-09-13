using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Common.Models.ApplicationOptions;

public class LicenseInfoOptions
{
    #region Public Fields

    public const string Section = "LicenseInfo";

    #endregion Public Fields

    #region Public Properties

    public string ProductName { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;

    #endregion Public Properties
}
