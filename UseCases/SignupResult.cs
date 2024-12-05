using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class SignupResult
    {
        public SignupResultCodes ResultCode { get; }
        public string Message { get; } = string.Empty;

        public SignupResult(SignupResultCodes resultCode, string message) 
        {       
            ResultCode = resultCode;
            Message = message;
        }

        public static SignupResult UserExisted = new(SignupResultCodes.UserExisted, "User already exists!");
        public static SignupResult Success = new(SignupResultCodes.Success, "Success!");
    }
}
