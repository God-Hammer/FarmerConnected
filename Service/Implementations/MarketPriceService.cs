using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class MarketPriceService : BaseService, IMarketPriceService
    {
        private readonly IMarketPriceRepository _marketPriceRepository;
        public MarketPriceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _marketPriceRepository = unitOfWork.MarketPrice;
        }

        public async Task<List<MarketPriceViewModel>> GetMarketPrices()
        {
            return await _marketPriceRepository.GetAll()
                                                .ProjectTo<MarketPriceViewModel>(_mapper.ConfigurationProvider)
                                                .ToListAsync();
        }

        public async Task<MarketPriceViewModel?> GetMarketPrice(Guid id)
        {
            return await _marketPriceRepository.GetMany(market => market.Id.Equals(id))
                .ProjectTo<MarketPriceViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<MarketPriceViewModel?> CreateMarketPrice(MarketPriceRequest request)
        {
            var marketPrice = new MarketPrice
            {
                Id = Guid.NewGuid(),
                ProductName = request.ProductName,
                Price = request.Price,
            };

            _marketPriceRepository.Add(marketPrice);
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return await GetMarketPrice(marketPrice.Id);
            }
            return null;
        }

        public async Task<MarketPriceViewModel?> UpdateMarketPrice(Guid id, UpdateMarketPriceRequest request)
        {
            var marketPrice = await _marketPriceRepository.GetMany(market => market.Id.Equals(id)).FirstOrDefaultAsync();
            if (marketPrice == null) return null;

            marketPrice.ProductName = request.ProductName ?? marketPrice.ProductName;
            marketPrice.Price = request.Price ?? marketPrice.Price;

            marketPrice.UpdateAt = DateTime.Now;

            _marketPriceRepository.Update(marketPrice);
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return await GetMarketPrice(marketPrice.Id);
            }
            return null;

        }
    }
}
