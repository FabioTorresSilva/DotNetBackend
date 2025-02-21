using Microsoft.Extensions.Hosting;
using Radao.Data;
using Radao.Models;

namespace Radao.Services
{
    public class BackgroundServiceContinuousUseDevice : BackgroundService
    {
        private readonly ILogger<BackgroundServiceContinuousUseDevice> _logger;

        private readonly ContinuousUseDeviceService continuousUseDeviceService;

        private readonly WaterAnalysisService waterAnalysisService;

        private readonly RadaoContext _context;

        public BackgroundServiceContinuousUseDevice(ILogger<BackgroundServiceContinuousUseDevice> logger, 
            ContinuousUseDeviceService continuousUseDeviceService, WaterAnalysisService waterAnalysisService, RadaoContext context)
        {
            _logger = logger;
            this.continuousUseDeviceService = continuousUseDeviceService;
            this.waterAnalysisService = waterAnalysisService;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Task executed at: {time}", DateTimeOffset.Now);

                List<ContinuousUseDevice> devices = await continuousUseDeviceService.GetContinuousUseDevices();

                // Gets the current date
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                foreach (ContinuousUseDevice cd in devices)
                {
                    // Ensures Device is attatched to a fountain. If not, continues to the next iteration.
                    if (cd.Fountain == null)
                        continue;

                    // Gets number of days passed since last analysis
                    int daysPassed = today.DayNumber - cd.LastAnalysisDate.DayNumber;

                    // Ensures that number of days passed is equal or bigger than the frequency
                    if(daysPassed >= cd.AnalysisFrequency)
                    {
                        // Creates and adds new WaterAnalysis
                        WaterAnalysis waterAnalysis = new WaterAnalysis(new Random().Next(0, 201), ((int)cd.Id), today, cd.Id);
                        waterAnalysisService.AddWaterAnalysisAsync(waterAnalysis);
                        
                        // Updates LastAnalysisDate on the ContinuousUseDevice
                        cd.LastAnalysisDate = today;
                        await _context.SaveChangesAsync();

                    }

                }
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Wait 24 hours
            }
        }
    }
}
