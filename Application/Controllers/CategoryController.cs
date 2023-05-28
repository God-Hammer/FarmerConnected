using Data.Models.Requests.Post;
using Data.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Constant;

namespace Application.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategories();
            if (result == null || result.Count <= 0)
            {
                return NotFound(ResponseMessage.CategoryNotFound);
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var result = await _categoryService.CreateCategory(request);
            if(request != null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return BadRequest(ResponseMessage.Error);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, CreateCategoryRequest request)
        {
            var result = await _categoryService.UpdateCategory(id, request);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return NotFound(ResponseMessage.ProductNotFound);
        }




    }
}
