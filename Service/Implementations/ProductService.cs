using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Filters;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Utility.Constant;

namespace Service.Implementations
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _productRepository = unitOfWork.Product;
        }

        
        /// <summary>
        /// Get product with filer
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>listPRoduct</returns>
        public async Task<List<ProductViewModel>> GetProducts(ProductFilterModel filter)
        {
            var query = _productRepository.GetAll();
            if(filter.Name != null)
            {
                query = query.Where(product => product.Name.Contains(filter.Name));
            }
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(product => product.Price >= filter.MinPrice.Value);
            }
            if(filter.MaxPrice.HasValue)
            {
                query = query.Where(product => product.Price <= filter.MaxPrice.Value);
            }
            var listProducts = await query
                                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return listProducts;
        }

        public async Task<ProductViewModel?> CreateProduct(Guid userID, CreateProductRequest request)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                Description = request.Description,
                CustomerId = userID,
                CategoryId = request.CategoryId,
                Status = ProductStatus.OnSale,
            };
            _productRepository.Add(product);
            var result = await _unitOfWork.SaveChanges();
            if(result > 0)
            {
                return await GetProduct(product.Id);
            }
            return null;
        }

        public async Task<ProductViewModel?> UpdateProduct(Guid productID, UpdateProductRequest request)
        {
            var product = await _productRepository.GetMany(product => product.Id.Equals(productID)).FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }

            product.Name = request.Name ?? product.Name;
            product.Price = request.Price ?? product.Price;
            product.Quantity = request.Quantity ?? product.Quantity;
            product.Description = request.Description ?? product.Description;
            

            product.UpdateAt = DateTime.Now;

            _productRepository.Update(product);
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return await GetProduct(product.Id);
            }
            return null;
        }

        public async Task<IActionResult> DeleteProduct(Guid productID)
        {
            var product = await _productRepository.GetMany(product => product.Id.Equals(productID)).FirstOrDefaultAsync();
            if(product == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            product.Status = ProductStatus.Deleted;

            _productRepository.Update(product);
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public async Task<ProductViewModel?> GetProduct(Guid id)
        {
            var product = await _productRepository.GetMany(product => product.Id.Equals(id))
                                                   .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync();
            if(product != null)
            {
                return product;
            }
            return null;
        }
    }
}
