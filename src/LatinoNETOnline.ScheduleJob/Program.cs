using System.Threading.Tasks;
using GitHubActionSharp;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Infrastructure;
using LatinoNETOnline.ScheduleJob.Infrastructure.Providers;
using LatinoNETOnline.ScheduleJob.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GitHubActionContext actionContext = new GitHubActionContext(args);
            actionContext.LoadParameters();

            WorkflowsRequestsContext workflowsRequestsContext = new WorkflowsRequestsContext();

            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .AddGitHubClient(actionContext)
                .AddSingleton(actionContext)
                .AddSingleton(workflowsRequestsContext)
                .AddSingleton<IJobApplicationService, JobApplicationService>()
                .AddSingleton<IEventService, EventService>()
                .AddMediatR(typeof(Program))
                .BuildServiceProvider();

            var jobApplicationService = serviceProvider.GetService<IJobApplicationService>();

            await jobApplicationService.StartJob();
        }
    }
}
