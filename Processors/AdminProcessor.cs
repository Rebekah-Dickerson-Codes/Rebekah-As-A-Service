using Rebekah_As_A_Service.DataAccess;
using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.Processors
{
    public class AdminProcessor
    {
        private FactDBAccessor _dbAccessor = new();
        private CategoryDBAccessor _catAccessor = new();

        public async Task<FactResponse> GetFactByID(int factID)
        {
            return await _dbAccessor.GetFactByID(factID);
        }

        public async Task<FactResponse> UpdateFactByID(int factID, FactUpdateRequest request)
        {
            var factData = GetFactByID(factID);

            return null;

        }

        public async Task AddFactCategoryAsync(string categoryName)
        {
            await _catAccessor.CreateNewCategory(categoryName);
        }
    }
}
