using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class CreateOrderResult
    {
        public CreateOrderResultCodes ResultCode { get; }
        public string Message { get; } = string.Empty;

        public CreateOrderResult(CreateOrderResultCodes resultCode, string message)
        {
            ResultCode = resultCode;
            Message = message;
        }
    }
}
