using Data.Models.Filters;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;

namespace Service.Interfaces
{
    public interface IPostService
    {
        Task<PostViewModel?> GetPost(Guid id);
        Task<List<PostViewModel>> GetPosts(PostFilterModel filter);
        Task<PostViewModel?> CreatePost(CreatePostRequest request);
        Task<PostViewModel?> UpdatePost(Guid id, UpdatePostRequest request);
    }
}
