using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ShippingService.Core.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ShippingService.Api.BackgroundServices
{
    public class PingWebsiteBackgroundService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PingWebsiteBackgroundService> _logger;
        private readonly IOptions<PingWebsiteSettings> _configuration;

        public PingWebsiteBackgroundService(
            IHttpClientFactory httpClientFactory,
            ILogger<PingWebsiteBackgroundService> logger,
            IOptions<PingWebsiteSettings> configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(PingWebsiteBackgroundService)} running at '{DateTime.Now}', pinging '{_configuration.Value.Url}'");
                try
                {
                    using (var client = _httpClientFactory.CreateClient(nameof(PingWebsiteBackgroundService)))
                    {
                        var response = await client.GetAsync(_configuration.Value.Url, cancellationToken);
                        _logger.LogInformation($"Is {_configuration.Value.Url.Authority} responding: {response.IsSuccessStatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during ping");
                }
                await Task.Delay(TimeSpan.FromMinutes(_configuration.Value.TimeIntervalInMinutes), cancellationToken);
            }
        }
    }
}
