using Entities;
using Infrastructure.Models.ViewModels;
using UseCases;

namespace Infrastructure.Services
{
    public class FeedbackMappingService
    {
        private readonly IUserManager _userManager;

        public FeedbackMappingService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<FeedbackViewModel> MapToFeedbackViewModel(Feedback feedback)
        {
            var feedbackViewModel = new FeedbackViewModel
            {
                Id = feedback.Id,
                Comment = feedback.Comment ?? "",
                Rating = feedback.Rating ?? 0,
                Username = "Unknown",
                AncestorFeedbackId = feedback.AncestorFeedbackId,
                CreatedAt = feedback.CreatedAt
            };

            var user = await _userManager.GetByIdAsync(feedback.UserId);
            if (user != null)
            {
                feedbackViewModel.Username = user.Username;
            }

            return feedbackViewModel;
        }
    }
}
