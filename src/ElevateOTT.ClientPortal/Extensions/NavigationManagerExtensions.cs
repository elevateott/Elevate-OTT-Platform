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


    public static void Test(this NavigationManager navManager)
    {
        // https://testerdave-2022724195054337.localhost:44335/
        // http://testerdave-2022724195054337.localhost:44335/
        // https://testerdave-2022724195054337.elevateott.tv
        // https://www.elevateott.tv/
        // http://www.elevateott.tv/
        // https://www.customdomain.com/
        // https://customsubdomain.customdomain.com
        // https://customsubdomain.customdomain.co.uk
        // https://www.customdomain.co.uk/
        // http://www.customdomain.co.uk/


        string url1 = "https://testerdave-2022724195054337.localhost:44335/";
        string url2 = "http://testerdave-2022724195054337.localhost:44335/";
        string url3 = "https://testerdave-2022724195054337.elevateott.tv";
        string url4 = "https://www.elevateott.tv/";
        string url5 = "http://www.elevateott.tv/";
        string url6 = "https://www.customdomain.com/";
        string url7 = "https://customsubdomain.customdomain.com";
        string url8 = "https://customsubdomain.customdomain.co.uk";
        string url9 = "https://www.customdomain.co.uk/";
        string url10 = "http://www.customdomain.co.uk/";



        // IsLocalHost       

        // IsCustomDomain

        // GetSubDomain

        // GetDomain
    }


    public static string GetSubDomain(this NavigationManager navManager)
    {
        var subDomain = navManager.BaseUri.Split('.')[0].Split("//")[1];

        return subDomain;
    }

    public static string GetDomain(this NavigationManager navManager)
    {
        // TODO get ParentDomain from config
        // http://
        // www.mysub.elevatott.tv
        // www.customdomain.com


        var domainSplit = navManager.BaseUri.Split("//")[1];
        string domainName = string.Empty;
        if (domainSplit.StartsWith("www."))
        {
            domainName = domainSplit.Substring(4);
        }

        return domainName.TrimEnd('/');
    }

    public static bool IsLocalHost(this NavigationManager navManager)
    {
        return navManager.BaseUri.ToLower().Contains("localhost:");
    }

    public static bool IsCustomDomain(this NavigationManager navManager)
    {
        // TODO get ParentDomain from config
        // TODO tenant could use their own custom subdomain!!

        string rootDomain = "elevateott.tv";
        return !navManager.BaseUri.ToLower().Contains(rootDomain);
    }

    #endregion Public Methods
}
