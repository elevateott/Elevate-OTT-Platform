using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevateOTT.Application.Common.Interfaces.Services.UtilityServices;

namespace ElevateOTT.Infrastructure.Services.UtilityServices;
public class CryptoService : ICryptoService
{
    private readonly string _iv;
    private readonly string _secretKey;

    private readonly IConfigReaderService _configReaderService;

    public CryptoService(IConfigReaderService configReaderService)
    {
        _configReaderService = configReaderService;
        var options = _configReaderService.GetCryptoOptions();
        _iv = options.CryptoIV;
        _secretKey = options.CryptoSecretKey;
    }

    public string EncryptText(string textToEncrypt)
    {
        var buffer = Encoding.UTF8.GetBytes(textToEncrypt);
        var ivBytes = Encoding.UTF8.GetBytes(_iv);
        var secretKeyBytes = Encoding.UTF8.GetBytes(_secretKey);

        var tripleDes = new TripleDESCryptoServiceProvider
        {
            IV = ivBytes,
            Key = secretKeyBytes,
        };

        ICryptoTransform transform = tripleDes.CreateEncryptor();

        var cipherText = transform.TransformFinalBlock(buffer, 0, buffer.Length);
        return Convert.ToBase64String(cipherText);
    }

    public string DecryptText(string cipheredText)
    {
        var buffer = Convert.FromBase64String(cipheredText);
        var ivBytes = Encoding.UTF8.GetBytes(_iv);
        var secretKeyBytes = Encoding.UTF8.GetBytes(_secretKey);

        var tripleDes = new TripleDESCryptoServiceProvider
        {
            IV = ivBytes,
            Key = secretKeyBytes
        };

        ICryptoTransform transform = tripleDes.CreateDecryptor();

        var plainText = transform.TransformFinalBlock(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(plainText);
    }
}
