using System;
namespace Cryptollet.Common.Models
{
    public class OnboardingItem
    {
        public OnboardingItem(string imageUrl, string title, string details)
        {
            ImageUrl = imageUrl;
            Title = title;
            Details = details;
        }

        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
