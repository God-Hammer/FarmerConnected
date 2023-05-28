using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Constant;

namespace Application.Controllers
{
    [Route("api/market-price")]
    [ApiController]
    public class MarketPriceController : ControllerBase
    {
        private readonly IMarketPriceService _marketPriceService;

        public MarketPriceController(IMarketPriceService marketPriceService)
        {
            _marketPriceService = marketPriceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<MarketPriceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketPrices()
        {
            var result = await _marketPriceService.GetMarketPrices();
            if(result == null || result.Count <= 0)
            {
                return NotFound(ResponseMessage.MarketPriceNotFound);
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MarketPriceViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMarketPrice([FromBody] MarketPriceRequest request)
        {
            var result = await _marketPriceService.CreateMarketPrice(request);
            if(result != null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return BadRequest(ResponseMessage.Error);
        }


        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(MarketPriceViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMarketPrice([FromRoute] Guid id, UpdateMarketPriceRequest request)
        {
            var result = await _marketPriceService.UpdateMarketPrice(id, request);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return NotFound(ResponseMessage.ProductNotFound);
        }
    }
}
