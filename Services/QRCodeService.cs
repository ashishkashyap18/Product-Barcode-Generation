using PBMS.Interface;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
namespace PBMS.Services
{
    public class QRCodeService : IQrCodeService
    {
        public byte[] GenerateQRCodeAsByteArray(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20);
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeAsBitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
