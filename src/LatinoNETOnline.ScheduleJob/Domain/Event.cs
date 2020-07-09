using System;

namespace LatinoNETOnline.ScheduleJob.Domain
{
    public class Event
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string Speaker { get; set; }
        public string EventbriteId { get; set; }
        public bool IsDraft { get; set; }
    }
}
