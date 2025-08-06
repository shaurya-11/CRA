using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using PatchAgentService.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PatchAgentService.Services
{
    public class PatchPollingWorker : BackgroundService
    {
        private readonly ILogger<PatchPollingWorker> _logger;
        private readonly PatchServiceClient _patchServiceClient;
        private readonly IConfiguration _config;

        public PatchPollingWorker(ILogger<PatchPollingWorker> logger, PatchServiceClient patchServiceClient, IConfiguration config)
        {
            _logger = logger;
            _patchServiceClient = patchServiceClient;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int customerId = int.Parse(_config["CustomerId"]!);
            int interval = int.Parse(_config["PollingIntervalSeconds"]!);

            _logger.LogInformation("Patch Agent started for CustomerId: {CustomerId}", customerId);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var patches = await _patchServiceClient.GetLatestPatchStatusesAsync(customerId);

                    foreach (var patch in patches)
                    {
                        if (patch.HasUpdate && patch.PatchId.HasValue)
                        {
                            _logger.LogInformation($"Found update for {patch.ProductName} (Version {patch.Version}). Installing...");
                            await Task.Delay(5000, stoppingToken); // Simulate download/install
                            _logger.LogInformation($"Installation complete for PatchId: {patch.PatchId}");

                            await _patchServiceClient.UpdateStatusAsync(customerId, patch.PatchId.Value, 2); // 2 = Installed
                            _logger.LogInformation($"Status updated to Installed for PatchId: {patch.PatchId}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during patch polling.");
                }

                await Task.Delay(interval * 1000, stoppingToken);
            }
        }
    }
}
