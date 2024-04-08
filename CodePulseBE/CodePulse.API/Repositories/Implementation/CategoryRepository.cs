using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string? query = null)
        {
            //Query
            var categories = dbContext.Categories.AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }
            return await categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var request = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (request != null)
            {
                dbContext.Entry(request).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var request = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (request is null)
            {
                return null;
            }

            dbContext.Categories.Remove(request);
            await dbContext.SaveChangesAsync();

            return request;
        }
    }
}
