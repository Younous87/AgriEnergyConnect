using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;
using Microsoft.EntityFrameworkCore;

namespace PROG7311_POE.Data.Repositories
{
    public class FarmerRepository : Repository<Farmer>, IFarmerRepository
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Constructor that passes the ApplicationDbContext to the base repository class
        public FarmerRepository(ApplicationDbContext context) : base(context) { }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves a farmer based on the associated user ID
        public async Task<Farmer> GetByUserIdAsync(int userId)
        {
            return await _context.Farmers
                .FirstOrDefaultAsync(f => f.UserId == userId);
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves all farmers along with their associated products
        public async Task<IEnumerable<Farmer>> GetFarmersWithProductsAsync()
        {
            return await _context.Farmers
                .Include(f => f.Products)
                .ToListAsync();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves a farmer by their FarmerId, including their related products
        public override async Task<Farmer> GetByIdAsync(int id)
        {
            return await _context.Farmers
                .Include(f => f.Products) 
                .FirstOrDefaultAsync(f => f.FarmerId == id);
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
