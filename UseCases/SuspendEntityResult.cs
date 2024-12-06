using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class SuspendEntityResult
    {
        public SuspendEntityResultCodes ResultCode { get; }
        public string Message { get; } = string.Empty;

        public SuspendEntityResult(SuspendEntityResultCodes resultCode, string message)
        {
            ResultCode = resultCode;
            Message = message;
        }

        public static SuspendEntityResult Success = new(SuspendEntityResultCodes.Success, string.Empty);
        public static SuspendEntityResult Error = new(SuspendEntityResultCodes.Error, string.Empty);
        public static SuspendEntityResult NotFound = new(SuspendEntityResultCodes.NotFound, string.Empty);
    }
}
