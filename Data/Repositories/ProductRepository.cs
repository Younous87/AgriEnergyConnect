using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;
using Microsoft.EntityFrameworkCore;

namespace PROG7311_POE.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .ToListAsync();
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetByFarmerIdAsync(int farmerId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();
        }

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

        public async Task<IEnumerable<Product>> GetRecentProductsAsync(int count)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }
    }
}
