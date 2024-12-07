using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer.Repositories.SqlServer.DataContext
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public required int Quantity { get; set; }
        public int CartId { get; set; }

        public Cart Cart { get; set; } = default!;
        public Book Book { get; set; } = default!;
    }
}
