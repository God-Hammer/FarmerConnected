using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.Views;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthViewModel> AuthenticatedUser(AuthRequest auth);
        Task<AuthModel?> GetCustomerById(Guid id);
    }
}
