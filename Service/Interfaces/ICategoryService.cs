using Data.Models.Requests.Post;
using Data.Models.Views;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryViewModel?> GetCategory(Guid id);
        Task<List<CategoryViewModel>> GetCategories();
        Task<CategoryViewModel?> CreateCategory(CreateCategoryRequest request);
        Task<CategoryViewModel?> UpdateCategory(Guid categoryID, CreateCategoryRequest request);
    }
}
