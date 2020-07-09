using System.Threading.Tasks;
using GitHubActionSharp;
using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly ILogger<JobApplicationService> _logger;
        private readonly GitHubActionContext _gitHubActionContext;
        private readonly WorkflowsRequestsContext _workflowsRequestsContext;
        private readonly IMediator _mediator;

        public JobApplicationService(ILoggerFactory loggerFactory, GitHubActionContext gitHubActionContext, WorkflowsRequestsContext workflowsRequestsContext, IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<JobApplicationService>();
            _gitHubActionContext = gitHubActionContext;
            _workflowsRequestsContext = workflowsRequestsContext;
            _mediator = mediator;
        }

        public async Task StartJob()
        {
            _logger.LogInformation("Starting application");

            string workflow = _gitHubActionContext.GetParameter(Parameters.Workflow).Trim();

            _logger.LogInformation($"Received workflow name: {workflow}");

            IRequest request = _workflowsRequestsContext.GetRequest(workflow);

            await _mediator.Send(request);

            _logger.LogInformation("All done!");
        }

    }
}
