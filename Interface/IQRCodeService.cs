namespace PBMS.Interface
{
    public interface IQrCodeService
    {
        byte[] GenerateQRCodeAsByteArray(string text);
    }
}
