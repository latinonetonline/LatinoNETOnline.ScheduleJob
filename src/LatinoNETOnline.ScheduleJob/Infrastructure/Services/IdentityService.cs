using System;
using System.Net.Http;
using System.Threading.Tasks;

using GitHubActionSharp;

using IdentityModel.Client;

using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;

using Microsoft.Extensions.Logging;

using static IdentityModel.OidcConstants;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IdentityService> _logger;
        private readonly string _identityClientSecret;

        public IdentityService(IHttpClientFactory _httpClientFactory, GitHubActionContext gitHubActionContext, ILoggerFactory loggerFactory)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _identityClientSecret = gitHubActionContext.GetParameter(Parameters.IdentityClientSecret);
            _logger = loggerFactory.CreateLogger<IdentityService>();
        }

        public async Task<string> GetAccessToken()
        {
            _logger.LogInformation("Starting Get AccessToken from https://latinonetonlineidentityserver.herokuapp.com");
            string host = "https://latinonetonlineidentityserver.herokuapp.com";
            DiscoveryDocumentResponse discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(
            new DiscoveryDocumentRequest
            {
                Address = host,
                Policy =
                {
                    ValidateIssuerName = false
                }
            });
            if (discoveryDocument.IsError)
            {
                throw new Exception("error");
            }

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                GrantType = GrantTypes.ClientCredentials,
                ClientId = "schedule-job-client",
                ClientSecret = _identityClientSecret
            };

            IdentityModel.Client.TokenResponse TokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (TokenResponse.IsError)
            {
                throw new Exception("error");
            }

            _logger.LogInformation("Get Access Token successfuly");

            return TokenResponse.AccessToken;
        }
    }
}
