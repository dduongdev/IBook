using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public interface IAuthenticationManager
    {
        Task<LoginResult> LoginAsync(string username, string password);
        Task<SignupResult> SignupAsync(User user);
    }
}
