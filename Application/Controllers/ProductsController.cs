using Application.Configurations.Middleware;
using Data.Models.Filters;
using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Constant;

namespace Application.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilterModel filter)
        {
            var listProducts = await _productService.GetProducts(filter);
            if(listProducts == null || listProducts.Count == 0)
            {
                return NotFound("List products is empty.");
            }
            return Ok(listProducts);
        }

        [HttpPost]
        [Authorize(UserRole.Farmer)]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var user = (AuthModel)HttpContext.Items["User"]!;
            if(user != null)
            {
                var category = await _categoryService.GetCategory(request.CategoryId);
                if(category != null)
                {
                    var result = await _productService.CreateProduct(user.Id, request);
                    if(result != null)
                    {
                        return StatusCode(StatusCodes.Status201Created, result);
                    }
                    return BadRequest(ResponseMessage.Error);
                }
                return NotFound(ResponseMessage.CategoryNotFound);
            }
            return Unauthorized();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
        {
            var result = await _productService.UpdateProduct(id, request);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return NotFound(ResponseMessage.ProductNotFound);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            if(result is StatusCodeResult statusCodeResult){
                var statusCode = statusCodeResult.StatusCode;
                if(statusCode == StatusCodes.Status404NotFound)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseMessage.ProductNotFound);
                }
                if(statusCode == StatusCodes.Status204NoContent)
                {
                    return StatusCode(StatusCodes.Status204NoContent, ResponseMessage.Success);
                }
                
            }
            return BadRequest(ResponseMessage.Error);
        }

    }
}
