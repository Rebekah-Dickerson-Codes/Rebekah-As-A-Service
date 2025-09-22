using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.DataAccess.Interfaces
{
    public interface ICategoryDBAccessor
    {
        Task CreateNewCategory(string categoryName);
        Task<List<CategoryResponse>> GetCategories();
    }
}
