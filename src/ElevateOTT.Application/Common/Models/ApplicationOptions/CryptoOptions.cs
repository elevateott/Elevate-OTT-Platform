using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Application.Common.Models.ApplicationOptions;
public class CryptoOptions
{
    #region Public Fields

    public const string Section = "Crypto";

    #endregion Public Fields

    #region Public Properties

    public string CryptoSecretKey { get; set; } = string.Empty;
    public string CryptoIV { get; set; } = string.Empty;

    #endregion Public Properties
}
