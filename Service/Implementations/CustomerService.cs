using AutoMapper;
using Data;
using Data.Entities;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.Views;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System.Security.Cryptography;
using Utility.Constant;

namespace Service.Implementations
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISendMailService _sendMailService;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ISendMailService sendMailService) : base(unitOfWork, mapper)
        {
            _customerRepository = unitOfWork.Customer;
            _sendMailService = sendMailService;
        }
        public async Task<CustomerViewModel> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
            if (customer != null)
            {
                return new CustomerViewModel
                {
                    Id = customer.Id,
                    Address = customer.Address,
                    AvatarUrl = customer.AvatarUrl,
                    Email = customer.Email,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    CreateAt = customer.CreateAt,
                    IsActive = StatusOfUser(customer.IsActive),

                };
            }
            return null!;
        }

        public async Task<IActionResult> RegisterCustomer(RegisterRequest request)
        {

            if (_customerRepository.Any(c => c.Email.Equals(request.Email)))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Password = request.Password,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                VerifyToken = CreateRandomToken(),
                IsActive = false,
                CreateAt = DateTime.Now
            };

            _customerRepository.Add(customer);
            await _unitOfWork.SaveChanges();

            await _sendMailService.SendVerificationEmail(customer.Email, customer.VerifyToken);

            return new StatusCodeResult(StatusCodes.Status201Created);
        }


        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerRequest request)
        {
            var existingCustomer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
            if (existingCustomer == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            }

            existingCustomer.Name = request.Name;
            existingCustomer.Phone = request.Phone;
            existingCustomer.Address = request.Address;

            _customerRepository.Update(existingCustomer);
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);

        }



        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
            if (customer == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            customer.IsActive = false;
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public async Task<IActionResult> UpdateCustomerPassword(Guid id, UpdatePasswordRequest request)
        {
            var customer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();

            if (customer == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            if (!customer.Password.Equals(request.OldPassword))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            customer.Password = request.NewPassword;

            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);

        }



        public async Task<IActionResult> VerifyCustomer(string token)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.VerifyToken.Equals(token));
            if (customer == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);

            }
            customer.VerifyTime = DateTime.Now;
            customer.IsActive = true;


            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);

        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }


        private UserStatus? StatusOfUser(bool? isActive)
        {
            UserStatus? status = null;
            if (isActive.HasValue)
            {
                if (isActive.Value)
                {
                    status = UserStatus.Activated;
                }
                else
                {
                    status = UserStatus.DeActivated;
                }
            }
            return status;
        }
    }
}
