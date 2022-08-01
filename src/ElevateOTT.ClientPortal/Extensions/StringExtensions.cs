namespace ElevateOTT.ClientPortal.Extensions;

public static class StringExtensions
{
    #region Public Methods

    public static string Filter(this string str, List<char> charsToRemove)
    {
        return charsToRemove.Aggregate(str, (current, c) => current.Replace(c.ToString(), string.Empty));
    }

    public static string ReplaceSpaceAndSpecialCharsWithDashes(this string str)
    {
        var cleanedStr = Regex.Replace(str, "[^a-zA-Z0-9_.-]+", "-", RegexOptions.Compiled).Replace(" ", "-");
        return cleanedStr;
    }

    public static string FormatSlug(this string str)
    {
        return ReplaceSpaceAndSpecialCharsWithDashes(str).ToLower();
    }

    #endregion Public Methods
}
