using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Workflows.Emply
{
    public class EmplyHandler : AsyncRequestHandler<EmplyRequest>
    {
        private readonly ILogger<EmplyHandler> _logger;

        public EmplyHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmplyHandler>();
        }

        protected override Task Handle(EmplyRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Emply Workflow");
            return Task.CompletedTask;
        }
    }
}
