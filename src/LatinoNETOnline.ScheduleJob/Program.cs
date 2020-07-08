using GitHubActionSharp;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob
{
    class Program
    {
        static void Main(string[] args)
        {
            GitHubActionContext actionContext = new GitHubActionContext(args);
            actionContext.LoadParameters();

            WorkflowsRequestsContext workflowsRequestsContext = new WorkflowsRequestsContext();

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .AddSingleton(actionContext)
                .AddSingleton(workflowsRequestsContext)
                .AddSingleton<IJobApplicationService, JobApplicationService>()
                .AddMediatR(typeof(Program))
                .BuildServiceProvider();

            var jobApplicationService = serviceProvider.GetService<IJobApplicationService>();

            jobApplicationService.StartJob();
        }
    }
}
