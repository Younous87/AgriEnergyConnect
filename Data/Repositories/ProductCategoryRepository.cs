using PROG7311_POE.Data.Repositories.Interfaces;
using PROG7311_POE.Models;

namespace PROG7311_POE.Data.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}
