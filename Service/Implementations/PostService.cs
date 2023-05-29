using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Filters;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class PostService : BaseService, IPostService
    {
        private IPostRepository _postRepository;
        public PostService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _postRepository = unitOfWork.Post;
        }

        public async Task<PostViewModel?> GetPost(Guid id)
        {
            return await _postRepository.GetMany(post => post.Id.Equals(id))
                                            .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
                                            .FirstOrDefaultAsync();
        }

        public async Task<List<PostViewModel>> GetPosts(PostFilterModel filter)
        {
            var query = _postRepository.GetAll();
            if(filter.Title != null)
            {
                query = query.Where(post => post.Title.Contains(filter.Title));
            }
            if(filter.ProductName != null)
            {
                query = query.Include(post => post.MarketPrice)
                             .Where(post => post.MarketPrice.ProductName.Contains(filter.ProductName));
            }
            return await query.ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
        }

        public async Task<PostViewModel?> CreatePost(CreatePostRequest request)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                MarketPriceId = request.MarketPriceId,
                Description = request.Description,
            };

            _postRepository.Add(post);
            var result = await _unitOfWork.SaveChanges();
            if(result > 0)
            {
                return await GetPost(post.Id);
            }
            return null;
        }

        public async Task<PostViewModel?> UpdatePost(Guid id, UpdatePostRequest request)
        {
            var post = await _postRepository.GetMany(product => product.Id.Equals(id)).FirstOrDefaultAsync();
            if(post == null) return null;

            post.Title = request.Title ?? post.Title;
            post.Description = request.Description ?? post.Description;

            post.UpdateAt = DateTime.Now;

            _postRepository.Update(post);
            var result = await _unitOfWork.SaveChanges();
            if(result > 0)
            {
                return await GetPost(post.Id);
            }
            return null;
        }
    }
}
