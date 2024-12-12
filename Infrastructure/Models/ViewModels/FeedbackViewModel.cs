using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.ViewModels
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public required int Rating { get; set; }
        public string Username { get; set; } = string.Empty;
        public int? AncestorFeedbackId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Danh sách các phản hồi
        public List<FeedbackViewModel> Replies { get; set; } = new List<FeedbackViewModel>();
    }
}
