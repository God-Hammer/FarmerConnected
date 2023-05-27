using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerViewModel> GetCustomer(Guid id);
        Task<IActionResult> RegisterCustomer(RegisterRequest request);
        Task<IActionResult> VerifyCustomer(string token);
        Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerRequest request);
        Task<IActionResult> DeleteCustomer(Guid id);
        Task<IActionResult> UpdateCustomerPassword(Guid id, UpdatePasswordRequest request);
    }
}
