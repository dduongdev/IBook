using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer.Repositories.SqlServer.DataContext
{
    public class Feedback
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public required int Rating { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int? AncestorFeedbackId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Book Book { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}
