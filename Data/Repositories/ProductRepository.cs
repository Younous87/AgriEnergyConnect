using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;
using Microsoft.EntityFrameworkCore;

namespace PROG7311_POE.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Constructor that passes the ApplicationDbContext to the base repository
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves all products including their associated category and farmer data
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .ToListAsync();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves a specific product by its ID including related category and farmer info
        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves all products belonging to a specific farmer
        public async Task<IEnumerable<Product>> GetByFarmerIdAsync(int farmerId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Filters products based on various optional parameters such as category, date range, organic status, and farmer ID
        public async Task<IEnumerable<Product>> FilterProductsAsync(int? categoryId, DateTime? startDate, DateTime? endDate, bool organicOnly, int? farmerId)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate <= endDate.Value);
            }

            if (organicOnly)
            {
                query = query.Where(p => p.IsOrganic);
            }

            if (farmerId.HasValue)
            {
                query = query.Where(p => p.FarmerId == farmerId.Value);
            }

            return await query.ToListAsync();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

        // Retrieves the most recently added products, limited by the provided count
        public async Task<IEnumerable<Product>> GetRecentProductsAsync(int count)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        //°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//

    }
}//°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°...ooo000 END OF FILE 000ooo...°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°//
