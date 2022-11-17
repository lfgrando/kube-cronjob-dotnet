namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Worker running in container at: {time}", DateTimeOffset.Now);

                //throw new Exception("forced error");

                // Execute some business processing.
                await Task.Delay(5000, stoppingToken);

                _logger.LogInformation("Finished processing at: {time}.", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred at cron job app execution.");
                throw;
            }
            finally
            {
                _logger.LogInformation("Forcing stop at: {time}.", DateTimeOffset.Now);
                _hostApplicationLifetime.StopApplication();
            }
        }
    }
}