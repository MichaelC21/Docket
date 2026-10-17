using Docket.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Docket.Worker.Services;

public class WorkerService : BackgroundService
{
    private TimeSpan poll = TimeSpan.FromMilliseconds(5000);
    private ILogger<WorkerService> _logger;    
    public WorkerService(ILogger<WorkerService> logger, IConfiguration configuration)
    {
        _logger = logger;

        var pollInterval = configuration.GetValue<int>("Worker:PollInterval");
        poll = pollInterval <= 0 ?  poll : TimeSpan.FromMilliseconds(pollInterval);
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation($"Worker: polling");
                await Task.Delay(poll, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Worker: {ex.Message}");
            }

        }
    }
}
