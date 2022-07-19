namespace ElevateOTT.Application.Common.Interfaces.Services.UtilityServices;

public interface ICryptoService
{
    string EncryptText(string textToEncrypt);
    string DecryptText(string cipheredText);
}
