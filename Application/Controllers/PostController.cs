using Data.Models.Filters;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Constant;

namespace Application.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMarketPriceService _marketPriceService;
        private readonly IPostService _postService;

        public PostController(IPostService postService, IMarketPriceService marketPriceService)
        {
            _marketPriceService = marketPriceService;
            _postService = postService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PostViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPosts([FromQuery] PostFilterModel filter)
        {
            var result = await _postService.GetPosts(filter);
            if(result == null || result.Count <= 0)
            {
                return NotFound(ResponseMessage.PostNotFound);
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var flag = await _marketPriceService.GetMarketPrice(request.MarketPriceId);
            if (flag == null) return NotFound(ResponseMessage.MarketPriceNotFound);

            var result = await _postService.CreatePost(request);
            if(request != null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return BadRequest(ResponseMessage.Error);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(PostViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest request)
        {
            var result = await _postService.UpdatePost(id, request);
            if(result != null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return NotFound(ResponseMessage.PostNotFound);
        }
    }
}
