using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public enum CreateOrderResultCodes
    {
        Success,
        BookOutOfStock,
        BookStockTooLow,
        BookIsSuspended,
        BookNotFound,
        Error
    }
}
