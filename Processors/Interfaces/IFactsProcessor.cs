using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.Processors.Interfaces
{
    public interface IFactsProcessor
    {
        public Task<List<FactResponse>> GetFactsByCategory(string factCategory);

        public Task<List<CategoryResponse>> GetCategoriesAsync();
    }
}
