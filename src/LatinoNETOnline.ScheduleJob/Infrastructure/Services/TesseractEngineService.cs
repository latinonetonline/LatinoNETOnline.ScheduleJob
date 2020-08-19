using System;
using System.Net.Http;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;

using Tesseract;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class TesseractEngineService : ITesseractEngineService
    {
        private readonly HttpClient _httpClient;

        public TesseractEngineService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> ReadImageText(Uri uri)
        {
            using (var engine = new TesseractEngine(@"../../Assets/TesseractEngine", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromMemory(await _httpClient.GetByteArrayAsync(uri.ToString())))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }
    }
}
