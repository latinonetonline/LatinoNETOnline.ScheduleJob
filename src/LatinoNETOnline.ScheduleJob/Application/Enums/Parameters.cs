using GitHubActionSharp;

namespace LatinoNETOnline.ScheduleJob.Application.Enums
{
    public enum Parameters
    {
        [Parameter("workflow")]
        Workflow,

        [Parameter("twitter-consumer-id")]
        TwitterConsumerId,

        [Parameter("twitter-consumer-secret")]
        TwitterConsumerSecret,

        [Parameter("twitter-access-token")]
        TwitterAccessToken,

        [Parameter("twitter-access-token-secret")]
        TwitterAccessTokenSecret,

        [Parameter("github-access-token")]
        GitHubAccessToken,
    }
}
