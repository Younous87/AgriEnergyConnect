using PROG7311_POE.Models;

namespace PROG7311_POE.Data.Repositories.Interfaces
{
    public interface IFarmerRepository : IRepository<Farmer>
    {
        Task<Farmer> GetByUserIdAsync(int userId);
        Task<IEnumerable<Farmer>> GetFarmersWithProductsAsync();
    }
}
