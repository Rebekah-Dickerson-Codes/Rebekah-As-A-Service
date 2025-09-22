using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.Processors.Interfaces
{
    public interface IAdminProcessor
    {
        public Task<FactResponse?> UpdateFactByID(int factID, FactUpdateRequest request);
        public Task AddFactCategoryAsync(string categoryName);

        public Task<FactResponse> CreateNewFactAsync(FactCreateRequest request);
        public Task<FactResponse?> DeleteFactByIDAsync(int FactID);

    }
}
