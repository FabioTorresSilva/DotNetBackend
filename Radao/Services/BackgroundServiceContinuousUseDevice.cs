using Microsoft.Extensions.DependencyInjection; // Add this namespace
using Radao.Data;
using Radao.Models;
using Radao.Services.ServicesInterfaces;

namespace Radao.Services
{
    public class BackgroundServiceContinuousUseDevice : BackgroundService
    {
        private readonly ILogger<BackgroundServiceContinuousUseDevice> _logger;
        private readonly IServiceProvider _serviceProvider; // Store service provider

        public BackgroundServiceContinuousUseDevice(ILogger<BackgroundServiceContinuousUseDevice> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider; // Initialize service provider
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Task executed at: {time}", DateTimeOffset.Now);

                using (var scope = _serviceProvider.CreateScope()) // Create a new scope
                {
                    var continuousUseDeviceService = scope.ServiceProvider.GetRequiredService<IContinuousUseDeviceService>();
                    var waterAnalysisService = scope.ServiceProvider.GetRequiredService<IWaterAnalysisService>();
                    var context = scope.ServiceProvider.GetRequiredService<RadaoContext>();

                    List<ContinuousUseDevice> devices = await continuousUseDeviceService.GetContinuousUseDevices();

                    // Gets the current date
                    DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                    foreach (ContinuousUseDevice cd in devices)
                    {
                        // Ensures Device is attached to a fountain. If not, continues to the next iteration.
                        if (cd.Fountain == null)
                            continue;

                        // Gets number of days passed since last analysis
                        int daysPassed = today.DayNumber - cd.LastAnalysisDate.DayNumber;

                        // Ensures that number of days passed is equal or bigger than the frequency
                        if (daysPassed >= cd.AnalysisFrequency)
                        {
                            // Creates and adds new WaterAnalysis
                            WaterAnalysis waterAnalysis = new WaterAnalysis(new Random().Next(0, 201), ((int)cd.Id), today, cd.Id);
                            await waterAnalysisService.AddWaterAnalysisAsync(waterAnalysis);

                            // Updates LastAnalysisDate on the ContinuousUseDevice
                            cd.LastAnalysisDate = today;
                            await context.SaveChangesAsync();
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Wait 24 hours
            }
        }
    }
}
