using System;
using System.Threading.Tasks;

using GitHubActionSharp;

using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;

using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly ITwitterCredentials _credentials;

        public TwitterService(GitHubActionContext gitHubActionContext)
        {
            _credentials = new TwitterCredentials(gitHubActionContext.GetParameter(Parameters.TwitterConsumerId),
                gitHubActionContext.GetParameter(Parameters.TwitterConsumerSecret),
                gitHubActionContext.GetParameter(Parameters.TwitterAccessToken),
                gitHubActionContext.GetParameter(Parameters.TwitterAccessTokenSecret));
        }

        public async Task<Uri> CreateTweet(string tweetText, byte[] image)
        {
            ITwitterClient client = new TwitterClient(_credentials);

            var publishTweetParameters = new PublishTweetParameters(tweetText);
            if (image != null)
            {
                var media = await client.Upload.UploadBinaryAsync(image);
                publishTweetParameters.Medias.Add(media);
            }

            var publishedTweet = await client.Tweets.PublishTweetAsync(publishTweetParameters);

            return new Uri(publishedTweet.Url);
        }
    }
}
