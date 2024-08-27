using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PBMS.Db;
using PBMS.Interface;
using PBMS.Models;

namespace PBMS.Services
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContext _context;
        private readonly IQrCodeService _qrCodeService;

        public ProductServices(AppDbContext context, IQrCodeService qrCodeService)
        {

            _context = context;
            _qrCodeService = qrCodeService;
        }

        public async Task<bool> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productData = new
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Qty = product.Qty
            };
            string qrCodeDataJson = JsonConvert.SerializeObject(productData);
            product.QrCode = _qrCodeService.GenerateQRCodeAsByteArray(qrCodeDataJson);

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await GetProductById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product?>> GetAllProduct()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> Update(Product product)
        {
            var existProduct = await GetProductById(product.Id);
            if (existProduct != null)
            {
                _context.Entry(existProduct).CurrentValues.SetValues(product);

                var productData = new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Qty = product.Qty
                };
                string qrCodeDataJson = JsonConvert.SerializeObject(productData);
                existProduct.QrCode = _qrCodeService.GenerateQRCodeAsByteArray(qrCodeDataJson);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
    }
}
