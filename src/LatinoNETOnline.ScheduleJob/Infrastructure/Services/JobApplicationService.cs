using System;
using System.Text.Json;
using System.Threading.Tasks;
using GitHubActionSharp;
using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain.ObjectScheduleds;
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
        private readonly IObjectScheduledService _objectScheduledService;
        private readonly IEasyCronService _easyCronService;

        public JobApplicationService(ILoggerFactory loggerFactory, GitHubActionContext gitHubActionContext, HandlerRequestContext handlerRequestContext, IMediator mediator, IObjectScheduledService objectScheduledService, IEasyCronService easyCronService)
        {
            _logger = loggerFactory.CreateLogger<JobApplicationService>();
            _gitHubActionContext = gitHubActionContext;
            _handlerRequestContext = handlerRequestContext;
            _mediator = mediator;
            _objectScheduledService = objectScheduledService;
            _easyCronService = easyCronService;
        }

        public async Task StartJob()
        {
            _logger.LogInformation("Starting application");

            string handlerName = _gitHubActionContext.GetParameter(Parameters.HandlerName).Trim();

            _logger.LogInformation($"Received handler name: {handlerName}");

            IRequest request = await GetRequest(handlerName);

            await _mediator.Send(request);

            _logger.LogInformation("All done!");
        }

        private async Task<IRequest> GetRequest(string handlerName)
        {
            IRequest request = _handlerRequestContext.GetRequest(handlerName);

            if (request is IObjectScheduledRequest)
            {
                string objectScheduledId = _gitHubActionContext.GetParameter(Parameters.ObjectScheduledId).Trim();
                string easyCronId = _gitHubActionContext.GetParameter(Parameters.CronId).Trim();

                _logger.LogInformation($"Received object scheduled id: {objectScheduledId}");

                var fileContent = await _objectScheduledService.GetObjectScheduledAsync(Guid.Parse(objectScheduledId));

                if (fileContent is null || fileContent.Content is null)
                {
                    _logger.LogError($"There was an error getting the Object Scheduled");
                }

                _logger.LogInformation($"Object Scheduled: {fileContent.Content}");

                _logger.LogInformation($"Object Scheduled Type: {request.GetType().Name}");

                request = (IRequest)JsonSerializer.Deserialize(fileContent.Content, request.GetType());

                _logger.LogInformation($"Disabling Cron");

                await _easyCronService.DisableJob(long.Parse(easyCronId));

                _logger.LogInformation($"Cron Disabled");

                _logger.LogInformation($"Deleting Object Scheduled");

                await _objectScheduledService.DeleteObjectScheduledAsync(Guid.Parse(objectScheduledId));

                _logger.LogInformation($"Object Scheduled Deleted");
            }

            return request;
        }

    }
}
