using GitHubActionSharp;

namespace LatinoNETOnline.ScheduleJob.Application.Enums
{
    public enum Parameters
    {
        [Parameter("handler-name")]
        HandlerName,

        [Parameter("object-scheduled-id")]
        ObjectScheduledId,

        [Parameter("cron-id")]
        CronId,

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

        [Parameter("identity-client-secret")]
        IdentityClientSecret,

        [Parameter("easy-cron-client-secret")]
        EasyCronlientSecret,
    }
}
