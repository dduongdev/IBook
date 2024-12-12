using Infrastructure.Models.ViewModels;

namespace Infrastructure.Services
{
    public class FeedbackService
    {
        public IEnumerable<FeedbackViewModel> BuildFeedbackTree(IEnumerable<FeedbackViewModel> feedbackViewModels)
        {
            var lookup = feedbackViewModels.ToDictionary(_ => _.Id);
            var rootFeedbacks = new List<FeedbackViewModel>();

            foreach (var feedback in feedbackViewModels)
            {
                if (feedback.AncestorFeedbackId == null)
                {
                    rootFeedbacks.Add(feedback);
                }
                else if (lookup.TryGetValue(feedback.AncestorFeedbackId.Value, out var parent))
                {
                    parent.Replies.Add(feedback);
                }
            }

            return rootFeedbacks;
        }
    }
}
