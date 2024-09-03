using Application.Domain;
using Application.Domain.Context;
using Application.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        public readonly ApplicationDBContext _dBContext;
        public AuthRepository(ApplicationDBContext dBContext, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _configuration = configuration;
        }

        public async Task<SignInResponse?> SignIn(string? EmailId, string? Password)
        {
            try
            {

                var Result = await _dBContext
                    .User
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(EmailId.ToLower()) && x.Password.Equals(Password));

                if (Result == null)
                {
                    throw new Exception("User Not Found");
                }
                var authClaims = new List<Claim>
                {
                        new Claim("USER_ID", Result.Id.ToString()??""),
                        new Claim("USER_EMAIL_ID", Result.Email??""),
                        new Claim(ClaimTypes.Role, Result.Role.ToString())
                };

                var token = GetToken(authClaims);

                return new SignInResponse() { UserId = Result.Id, Email = Result.Email, Role = Result.Role, Token = token };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string?> SignUp(User request)
        {
            try
            {

                var IsUserExist = _dBContext.User.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());
                if (IsUserExist is not null)
                {
                    throw new Exception("User Already Exist");
                }

                var Result = _dBContext.User.Add(request);
                await _dBContext.SaveChangesAsync();
                
                return Result.Entity.Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }

    }
}
