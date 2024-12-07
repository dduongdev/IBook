using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.DTO
{
    public class OrderDTO
    {
        public Order Order { get; set; } = default!;
        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
