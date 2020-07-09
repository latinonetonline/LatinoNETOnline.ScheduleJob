using GitHubActionSharp;

namespace LatinoNETOnline.ScheduleJob.Application.Enums
{
    public enum Parameters
    {
        [Parameter("workflow")]
        Workflow,

        [Parameter("twitter-client-id")]
        TwitterClientId,

        [Parameter("twitter-client-secret")]
        TwitterClientSecret,

        [Parameter("twitter-access-token-public")]
        TwitterAccessTokenPublic,

        [Parameter("twitter-access-token-private")]
        TwitterAccessTokenPrivate,

        [Parameter("github-access-token")]
        GitHubAccessToken,
    }
}
