using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;

namespace Service.Interfaces
{
    public interface IMarketPriceService
    {
        Task<List<MarketPriceViewModel>> GetMarketPrices();
        Task<MarketPriceViewModel?> GetMarketPrice(Guid id);
        Task<MarketPriceViewModel?> CreateMarketPrice(MarketPriceRequest request);
        Task<MarketPriceViewModel?> UpdateMarketPrice(Guid id, UpdateMarketPriceRequest request);

    }
}
