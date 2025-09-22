using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.DataAccess.Interfaces
{
    public interface IFactDBAccessor
    {
        public Task<List<FactResponse>> GetFactsByCategory(string category);
        public Task<FactResponse> GetFactByID(int factID);
        public Task<FactResponse> UpdateFactByIdAsync(int factID, FactUpdateRequest request);
        public Task<FactResponse> InsertNewFactAsync(FactCreateRequest request);
    }
}
