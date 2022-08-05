using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Common.Models.ApplicationOptions;
public class TinyPNGOptions
{
    #region Public Fields

    public const string Section = "TinyPNG";

    #endregion Public Fields

    #region Public Properties

    public string ApiKey { get; set; } = string.Empty;

    #endregion Public Properties
}
