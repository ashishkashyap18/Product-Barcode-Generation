using PBMS.Models;

namespace PBMS.Interface
{
    public interface IProductServices
    {
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product?>> GetAllProduct();
        Task<bool> Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(int id);        
    }
}
