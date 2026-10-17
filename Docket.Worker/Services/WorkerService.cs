using Docket.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Docket.Worker.Services;

public class WorkerService : BackgroundService
{
    private TimeSpan timeout = TimeSpan.FromSeconds(5);

    private ILogger<WorkerService> _logger;
    private DocketDBContext _db;
    
    public WorkerService(ILogger<WorkerService> logger, DocketDBContext db, IConfiguration configuration)
    {
        _logger = logger;
        _db = db;

        var pollInterval = configuration.GetValue<int>("Worker:PollInterval");
        timeout = TimeSpan.FromMilliseconds(pollInterval);
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now}: Worker polling");
                await Task.Delay(timeout, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}: Error: {ex.Message}");
            }

        }
    }
}
