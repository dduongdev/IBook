using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public class AtomicTaskResult
    {
        public AtomicTaskResultCodes ResultCode { get; }
        public string Message { get; } = string.Empty;

        public AtomicTaskResult(AtomicTaskResultCodes resultCode, string message)
        {
            ResultCode = resultCode;
            Message = message;
        }

        public static AtomicTaskResult Success = new(AtomicTaskResultCodes.Success, string.Empty);
        public static AtomicTaskResult Error = new(AtomicTaskResultCodes.Error, string.Empty);
        public static AtomicTaskResult NotFound = new(AtomicTaskResultCodes.NotFound, string.Empty);
    }
}
