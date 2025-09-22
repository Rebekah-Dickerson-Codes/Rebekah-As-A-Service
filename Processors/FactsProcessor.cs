using Rebekah_As_A_Service.DataAccess.Interfaces;
using Rebekah_As_A_Service.Models;
using Rebekah_As_A_Service.Processors.Interfaces;

namespace Rebekah_As_A_Service.Processors
{
    public class FactsProcessor : IFactsProcessor
    {
        private readonly IFactDBAccessor _dbAccessor;
        private readonly ICategoryDBAccessor _categoryDBAccessor;

        public FactsProcessor(IFactDBAccessor dbAccessor, ICategoryDBAccessor categoryDBAccessor)
        {
            _dbAccessor = dbAccessor;
            _categoryDBAccessor = categoryDBAccessor;
        }
        public virtual async Task<List<FactResponse>> GetFactsByCategory(string factCategory)
        {
            var response =  await _dbAccessor.GetFactsByCategory(factCategory);
            return response;
        }

        public virtual async Task<List<CategoryResponse>> GetCategoriesAsync()
        {
            var response = await _categoryDBAccessor.GetCategories();
            return response;
        }
    }
}
