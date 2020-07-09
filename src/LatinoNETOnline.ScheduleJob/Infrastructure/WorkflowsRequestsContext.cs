using System;
using System.Collections.Generic;
using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Workflows.Emply;
using LatinoNETOnline.ScheduleJob.Application.Workflows.Thursday;
using MediatR;

namespace LatinoNETOnline.ScheduleJob.Infrastructure
{
    public class WorkflowsRequestsContext
    {
        public IRequest GetRequest(Workflows workflow)
        {
            return Pairs[workflow];
        }

        public IRequest GetRequest(string workflow)
        {
            Workflows workflowEnum = Enum.Parse<Workflows>(workflow);
            return Pairs[workflowEnum];
        }

        private readonly IReadOnlyDictionary<Workflows, IRequest> Pairs = new Dictionary<Workflows, IRequest>
            {
                { Workflows.Emply, new EmplyRequest() },
                { Workflows.Thursday, new ThursdayRequest() }
            };
    }
}
