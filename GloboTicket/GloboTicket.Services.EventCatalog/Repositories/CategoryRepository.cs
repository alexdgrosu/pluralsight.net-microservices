using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Services.EventCatalog.DbContexts;
using GloboTicket.Services.EventCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.EventCatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EventCatalogDbContext _dbContext;

        public CategoryRepository(EventCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories
                .ToArrayAsync();
        }

        public async Task<Category> GetCategoryById(string categoryId)
        {
            return await _dbContext.Categories
                .Where(x => x.CategoryId.ToString() == categoryId)
                .FirstOrDefaultAsync();
        }
    }
}
