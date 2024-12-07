using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer.Repositories.SqlServer.DataContext
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Entities.EntityStatus Status { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}
