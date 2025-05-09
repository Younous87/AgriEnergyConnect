using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;
using Microsoft.EntityFrameworkCore;

namespace PROG7311_POE.Data.Repositories
{
    public class FarmerRepository : Repository<Farmer>, IFarmerRepository
    {
        public FarmerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Farmer> GetByUserIdAsync(int userId)
        {
            return await _context.Farmers
                .FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task<IEnumerable<Farmer>> GetFarmersWithProductsAsync()
        {
            return await _context.Farmers
                .Include(f => f.Products)
                .ToListAsync();
        }

        public override async Task<Farmer> GetByIdAsync(int id)
        {
            return await _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.FarmerId == id);
        }
    }
}
