namespace BinaryPlate.BlazorPlate.Providers;

public static class ImageUploadConverter
{
    #region Public Methods

    public static string ToDataUrl(this MemoryStream data, string format)
    {
        var span = new Span<byte>(data.GetBuffer())[..(int)data.Length];
        return $"data:{format};base64,{Convert.ToBase64String(span)}";
    }

    public static byte[] ToBytes(string url)
    {
        var commaPos = url.IndexOf(',');
        if (commaPos < 0) return null;
        var base64 = url[(commaPos + 1)..];
        return Convert.FromBase64String(base64);
    }

    #endregion Public Methods
}