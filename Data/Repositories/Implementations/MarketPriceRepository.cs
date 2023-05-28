using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class MarketPriceRepository : Repository<MarketPrice>, IMarketPriceRepository
    {
        public MarketPriceRepository(FarmerConnectContext context) : base(context)
        {
        }
    }
}
