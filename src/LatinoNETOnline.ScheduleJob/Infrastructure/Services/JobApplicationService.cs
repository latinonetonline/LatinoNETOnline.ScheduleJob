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
        private readonly HandlerRequestContext _handlerRequestContext;
        private readonly IMediator _mediator;

        public JobApplicationService(ILoggerFactory loggerFactory, GitHubActionContext gitHubActionContext, HandlerRequestContext handlerRequestContext, IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<JobApplicationService>();
            _gitHubActionContext = gitHubActionContext;
            _handlerRequestContext = handlerRequestContext;
            _mediator = mediator;
        }

        public async Task StartJob()
        {
            _logger.LogInformation("Starting application");

            string handlerName = _gitHubActionContext.GetParameter(Parameters.HandlerName).Trim();

            _logger.LogInformation($"Received handler name: {handlerName}");

            IRequest request = _handlerRequestContext.GetRequest(handlerName);

            await _mediator.Send(request);

            _logger.LogInformation("All done!");
        }

    }
}
