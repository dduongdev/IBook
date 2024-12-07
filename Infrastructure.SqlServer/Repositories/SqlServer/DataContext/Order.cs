using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer.Repositories.SqlServer.DataContext
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Entities.OrderStatus Status { get; set; }
        public Entities.PaymentMethod PaymentMethod { get; set; }
        public Entities.PaymentStatus PaymentStatus { get; set; }
        public required string DeliveryAddress { get; set; }
        public required string DeliveryPhone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}
