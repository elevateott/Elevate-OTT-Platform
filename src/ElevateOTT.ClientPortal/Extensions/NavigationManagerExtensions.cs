using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace ElevateOTT.ClientPortal.Extensions;

public static class NavigationManagerExtensions
{
    #region Public Methods

    public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, out T value)
    {
        var uri = navManager.ToAbsoluteUri(navManager.Uri);

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
        {
            if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
            {
                value = (T)(object)valueAsInt;
                return true;
            }

            if (typeof(T) == typeof(string))
            {
                value = (T)(object)valueFromQueryString.ToString();
                return true;
            }

            if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
            {
                value = (T)(object)valueAsDecimal;
                return true;
            }
        }

        value = default;
        return false;
    }


    //
    // TODO
    // use for 2nd context for multi-tenant
    // tenant could use sub-domain as custom domain
    // Tenant name should not derive from domain or subdomain
    // since in this context, all sub domain will be the same 
    // ex: my.elevateott.tv

    public static bool IsBaseUriACustomDomain(this NavigationManager navManager)
    {
        // TODO check if url contains domain name
        // ex: contains("elevateott.tv")
        // put name in config

        string baseUri = navManager.BaseUri;
        if (string.IsNullOrEmpty(baseUri) || baseUri.Contains("localhost")) return false;

        var domainSplit = navManager.BaseUri.Split('.');
        return domainSplit.Length == 2;
    }

    public static string GetSubDomain(this NavigationManager navManager)
    {
        var subDomain = navManager.BaseUri.Split('.')[0].Split("//")[1];
        return subDomain;
    }

    public static string GetDomain(this NavigationManager navManager)
    {
        var domain = navManager.BaseUri.Split("//")[1];
        if (domain.StartsWith("www."))
        {
            domain = domain.Substring(5);
        }
        return domain;
    }

    #endregion Public Methods
}
