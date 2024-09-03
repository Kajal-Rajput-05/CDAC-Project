using Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Infrastructure
{
    public interface IAuthService
    {
        Task<string?> SignUp(User request);
        Task<SignInResponse?> SignIn(string? EmailId, string? Password);
    }
}
