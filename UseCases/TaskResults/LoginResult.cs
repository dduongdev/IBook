using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.TaskResults
{
    public class LoginResult
    {
        public LoginResultCodes ResultCode { get; }
        public User? User { get; }
        public string Message { get; } = string.Empty;

        public LoginResult(LoginResultCodes resultCode, User? user, string message)
        {
            ResultCode = resultCode;
            User = user;
            Message = message;
        }
    }
}
