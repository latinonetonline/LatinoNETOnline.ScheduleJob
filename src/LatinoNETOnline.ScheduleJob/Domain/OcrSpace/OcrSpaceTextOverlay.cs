using System.Collections.Generic;

namespace LatinoNETOnline.ScheduleJob.Domain.OcrSpace
{
    public class OCRSpaceTextOverlay
    {
        public List<OCRSpaceLine> Lines { get; set; }
        public bool HasOverlay { get; set; }
        public string Message { get; set; }
    }
}
