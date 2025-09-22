using Rebekah_As_A_Service.DataAccess.Interfaces;
using Rebekah_As_A_Service.Models;
using Rebekah_As_A_Service.Processors.Interfaces;

namespace Rebekah_As_A_Service.Processors
{
    public class AdminProcessor : IAdminProcessor
    {
        private readonly IFactDBAccessor _dbAccessor;
        private readonly ICategoryDBAccessor _catAccessor;

        public AdminProcessor(IFactDBAccessor factDBAccessor, ICategoryDBAccessor categoryDBAccessor)
        {
            _catAccessor = categoryDBAccessor;
            _dbAccessor = factDBAccessor;
        }

        public async Task<FactResponse> GetFactByID(int factID)
        {
            return await _dbAccessor.GetFactByID(factID);
        }

        public async Task<FactResponse> UpdateFactByID(int factID, FactUpdateRequest request)
        {
            var factData = GetFactByID(factID);

            //placeholder
            return null;

        }

        public async Task AddFactCategoryAsync(string categoryName)
        {
            //placeholder
            await _catAccessor.CreateNewCategory(categoryName);
        }
    }
}
