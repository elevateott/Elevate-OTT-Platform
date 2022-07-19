namespace ElevateOTT.Application.Common.Interfaces.Services.UtilityServices;

public interface IImageOptimizerService
{
    Task<byte[]> TinifyImage(IFormFile imageFile);
}
