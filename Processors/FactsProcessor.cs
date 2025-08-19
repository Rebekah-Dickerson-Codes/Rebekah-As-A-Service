using Rebekah_As_A_Service.DataAccess;
using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.Processors
{
    public class FactsProcessor
    {
        private DatabaseAccessor _dbAccessor = new();
        public async Task<FactResponse> GetFactByCategory(string factCategory)
        {
            return await _dbAccessor.GetRebekahFactByCategory(factCategory);
        }
    }
}
