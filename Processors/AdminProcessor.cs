using Rebekah_As_A_Service.DataAccess.Interfaces;
using Rebekah_As_A_Service.Models;
using Rebekah_As_A_Service.Processors.Interfaces;

namespace Rebekah_As_A_Service.Processors
{
    public class AdminProcessor : IAdminProcessor
    {
        private readonly IFactDBAccessor _dbAccessor;
        private readonly ICategoryDBAccessor _catAccessor;
        private readonly ILogger<AdminProcessor> _logger;

        public AdminProcessor(IFactDBAccessor factDBAccessor, ICategoryDBAccessor categoryDBAccessor, ILoggerFactory loggerFactory)
        {
            _catAccessor = categoryDBAccessor;
            _dbAccessor = factDBAccessor;
            _logger = loggerFactory.CreateLogger<AdminProcessor>();
        }

        private async Task<bool> FactIDExists(int factID)
        {
            var factId =  await _dbAccessor.GetFactByID(factID);
            if (factId == null)
            {
                return false;
            }
            return true;
        }

        public async Task<FactResponse?> UpdateFactByID(int factID, FactUpdateRequest request)
        {
            var factDataExists = await FactIDExists(factID);
            if (!factDataExists)
            {
                _logger.LogInformation($"No Fact Data returned from FactId {factID}");
                return null;
            }

            var updatedFact = await _dbAccessor.UpdateFactByIdAsync(factID, request);
            return updatedFact;
        }

        public async Task<FactResponse> CreateNewFactAsync(FactCreateRequest request)
        {
            var newFact = await _dbAccessor.InsertNewFactAsync(request);
            return newFact;
        }

        public async Task<FactResponse?> DeleteFactByIDAsync(int factID)
        {
            var isValidFactID = await FactIDExists(factID);
            if (!isValidFactID)
            {
                return null;
            }
            var newFact = await _dbAccessor.DeleteFactByID(factID);
            return newFact;
        }

        public async Task AddFactCategoryAsync(string categoryName)
        {
            //placeholder
            await _catAccessor.CreateNewCategory(categoryName);
        }
    }
}
