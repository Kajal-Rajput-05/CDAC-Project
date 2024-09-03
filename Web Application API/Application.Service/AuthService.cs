using Application.Domain;
using Application.Repository.Infrastructure;
using Application.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<SignInResponse?> SignIn(string? EmailId, string? Password)
        {
            return await _authRepository.SignIn(EmailId, Password);
        }

        public async Task<string?> SignUp(User request)
        {
            return await _authRepository.SignUp(request);
        }
    }
}
