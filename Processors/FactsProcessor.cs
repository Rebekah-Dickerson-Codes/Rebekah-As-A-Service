using Rebekah_As_A_Service.DataAccess;
using Rebekah_As_A_Service.Models;
using Rebekah_As_A_Service.Processors.Interfaces;

namespace Rebekah_As_A_Service.Processors
{
    public class FactsProcessor : IFactsProcessor
    {
        private FactDBAccessor _dbAccessor = new();
        private CategoryDBAccessor _categoryDBAccessor = new();
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
