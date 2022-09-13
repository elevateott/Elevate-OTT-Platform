namespace ElevateOTT.StreamingWebApp.Extensions;

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
    
    public static string GetSubDomain(this NavigationManager navManager)
    {
        var subDomain = navManager.BaseUri.Split('.')[0].Split("//")[1];
        return subDomain;
    }

<<<<<<< HEAD
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
            domainName = domainSplit.Substring(5);
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

=======
>>>>>>> parent of 536757c (started on free trial sign up flow)
    #endregion Public Methods
}