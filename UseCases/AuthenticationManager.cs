using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;

namespace UseCases
{
    public class AuthenticationManager
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            try
            {
                var storedUsers = await _userRepository.GetAllAsync();
                var targetUser = storedUsers.FirstOrDefault(_ => _.Username == username);

                if (targetUser == null)
                {
                    return new LoginResult(LoginResultCodes.UserNotFound, null, "User is not found!");
                }

                var passwordHasher = new PasswordHasher<string>();
                var verifyPasswordResult = passwordHasher.VerifyHashedPassword(username, targetUser.Password, password);
                if (verifyPasswordResult == PasswordVerificationResult.Failed)
                {
                    return new LoginResult(LoginResultCodes.WrongPassword, null, "Wrong password!");
                }

                return new LoginResult(LoginResultCodes.Success, targetUser, "Success!");
            }
            catch (Exception ex) 
            {
                return new LoginResult(LoginResultCodes.Error, null, ex.Message);
            }
        }
    }
}
