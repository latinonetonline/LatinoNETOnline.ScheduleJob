using System.Collections.Generic;

namespace LatinoNETOnline.ScheduleJob.Domain.OcrSpace
{
    public class OCRSpaceLine
    {
        public List<OCRSpaceWord> Words { get; set; }
        public int MaxHeight { get; set; }
        public int MinTop { get; set; }
    }
}
