using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using GitHubActionSharp;

using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain.OcrSpace;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class OcrService : IOcrService
    {
        private readonly HttpClient _httpClient;
        private readonly GitHubActionContext _gitHubActionContext;

        public OcrService(HttpClient httpClient, GitHubActionContext gitHubActionContext)
        {
            _httpClient = httpClient;
            _gitHubActionContext = gitHubActionContext;
        }

        public async Task<string> ReadImageText(Uri uri)
        {
            string apiKey = _gitHubActionContext.GetParameter(Parameters.OcrSpaceApiKey);

            string uriImage = HttpUtility.UrlEncode(uri.ToString());

            StringBuilder stringBuilder = new StringBuilder("parse/imageurl?");
            stringBuilder.Append($"apikey={apiKey}&");
            stringBuilder.Append($"url={uriImage}");

            OCRSpaceResponse response = await _httpClient.GetFromJsonAsync<OCRSpaceResponse>(stringBuilder.ToString());

            return response.ParsedResults.First().ParsedText;
        }
    }
}
