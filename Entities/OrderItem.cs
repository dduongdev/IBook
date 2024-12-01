using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required int Quantity { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc.")]
        public required decimal PriceAtPurchase { get; set; }
        public int OrderId { get; set; }
    }
}
