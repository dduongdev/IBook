using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer.Repositories.SqlServer.DataContext
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public required int Quantity { get; set; }
        public required decimal PriceAtPurchase { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = default!;
        public virtual Book Book { get; set; } = default!;
    }
}
