using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(FarmerConnectContext context) : base(context)
        {
        }
    }
}
