using Data.Models.Filters;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetProducts(ProductFilterModel filter);
        Task<ProductViewModel?> CreateProduct(Guid userID, CreateProductRequest request);
        Task<ProductViewModel?> UpdateProduct(Guid productID, UpdateProductRequest request);
        Task<IActionResult> DeleteProduct(Guid productID);
    }
}
