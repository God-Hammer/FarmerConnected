using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Requests.Post;
using Data.Models.Views;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class CategoryService : BaseService, ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _categoryRepository = unitOfWork.Category;
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await _categoryRepository.GetAll()
                .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CategoryViewModel?> GetCategory(Guid id)
        {
            var category = await _categoryRepository.GetMany(category => category.Id.Equals(id))
                                                   .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync();
            if (category != null)
            {
                return category;
            }
            return null;
        }


        public async Task<CategoryViewModel?> CreateCategory(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
            };

            _categoryRepository.Add(category);
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return await GetCategory(category.Id);
            }
            return null;
        }


        public async Task<CategoryViewModel?> UpdateCategory(Guid categoryID, CreateCategoryRequest request)
        {
            var category = await _categoryRepository.GetMany(category => category.Id.Equals(categoryID)).FirstOrDefaultAsync();
            if(category == null)
            {
                return null;
            }

            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;

            _categoryRepository.Update(category);

            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return await GetCategory(category.Id);
            }
            return null;
        }
    }
}
