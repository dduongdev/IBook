using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required string DeliveryPhone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public IEnumerable<OrderItem>? OrderItems { get; set; }
    }
}
