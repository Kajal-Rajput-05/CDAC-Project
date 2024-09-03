using Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Infrastructure
{
    public interface IAuthRepository
    {
        Task<string?> SignUp(User request);
        Task<SignInResponse?> SignIn(string? EmailId, string? Password);
    }
}
