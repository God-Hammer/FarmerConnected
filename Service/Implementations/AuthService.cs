﻿using AutoMapper;
using Data;
using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.Views;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility.Constant;
using Utility.Settings;

namespace Service.Implementations
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly AppSetting _appSettings;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSetting> appSettings) : base(unitOfWork, mapper)
        {
            _appSettings = appSettings.Value;
            _customerRepository = unitOfWork.Customer;
        }

        public async Task<AuthViewModel> AuthenticatedUser(AuthRequest auth)
        {
            var user = await _customerRepository.GetMany(user => user.Email.Equals(auth.Email) && user.Password.Equals(auth.Password)).FirstOrDefaultAsync();
            if (user != null)
            {
                var token = GenerateJwtToken(new AuthModel
                {
                    Id = user.Id,
                    Role = UserRole.Farmer.ToString()
                });

                return new AuthViewModel
                {
                    AccessToken = token
                };
            }
            return null!;
        }

        public async Task<AuthModel?> GetCustomerById(Guid id)
        {
            var customer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
            if (customer != null)
            {
                return new AuthModel
                {
                    Id = customer.Id,
                    Role = UserRole.Farmer.ToString(),
                };
            }
            return null!;
        }

        private string GenerateJwtToken(AuthModel auth)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", auth.Id.ToString()),

                    new Claim("role", auth.Role.ToString()),
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
