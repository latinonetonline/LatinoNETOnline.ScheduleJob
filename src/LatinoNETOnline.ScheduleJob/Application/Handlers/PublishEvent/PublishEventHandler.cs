﻿using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;

using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.PublishEvent
{
    class PublishEventHandler : AsyncRequestHandler<PublishEventRequest>
    {
        private readonly ILogger<PublishEventHandler> _logger;
        private readonly IEventService _eventService;
        private readonly ITesseractEngineService _tesseractEngineService;


        public PublishEventHandler(IEventService eventService, ILoggerFactory loggerFactory, ITesseractEngineService tesseractEngineService)
        {
            _eventService = eventService;
            _logger = loggerFactory.CreateLogger<PublishEventHandler>();
            _tesseractEngineService = tesseractEngineService;
        }

        protected override async Task Handle(PublishEventRequest request, CancellationToken cancellationToken)
        {
            var @event = await _eventService.Get(request.Date.Year, request.Date.Month, request.Guid);

            var text = await _tesseractEngineService.ReadImageText(new Uri(@event.ImageUrl));

            var results = DateTimeRecognizer.RecognizeDateTime(text, Culture.Spanish);


            _logger.LogInformation(results.Any() ? $"I found the following entities ({results.Count:d}):" : "I found no entities.");

            var resultJson = JsonSerializer.Serialize(results, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            if (text.ToLower().Contains(@event.Title.ToLower()))
            {
                _logger.LogInformation($"El título `{@event.Title}` se encuentra en la imagen.");
            }
            else
            {
                _logger.LogWarning($"El título `{@event.Title}` no coincide en la imagen.");
            }

            if (text.ToLower().Contains(@event.Speaker.ToLower()))
            {
                _logger.LogInformation($"El speaker `{@event.Speaker}` se encuentra en la imagen.");
            }
            else
            {
                _logger.LogWarning($"El speaker `{@event.Speaker}` no se encuentra en la imagen.");
            }

            if (resultJson.Contains(@event.Date.ToString("yyyy-dd-MM")))
            {
                _logger.LogInformation($"La fecha `{@event.Date.ToLongDateString()}` se encuentra en la imagen.");
            }
            else
            {
                _logger.LogError($"La fecha `{@event.Date.ToLongDateString()}` no se encuentra en la imagen.");
            }
        }
    }
}
