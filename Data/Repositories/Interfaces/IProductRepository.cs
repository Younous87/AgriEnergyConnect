using PROG7311_POE.Models;

namespace PROG7311_POE.Data.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByFarmerIdAsync(int farmerId);
        Task<IEnumerable<Product>> FilterProductsAsync(int? categoryId, DateTime? startDate, DateTime? endDate, bool organicOnly, int? farmerId);
        Task<IEnumerable<Product>> GetRecentProductsAsync(int count);
    }
}
