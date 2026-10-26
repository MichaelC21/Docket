using Docket.Worker.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Docket.Worker.Services;

public class WorkerService : BackgroundService
{
    private TimeSpan poll = TimeSpan.FromMilliseconds(5000);
    private ILogger<WorkerService> _logger;
    private IServiceScopeFactory _serviceScopeFactory;
    private string workerIdentity;
    public WorkerService(ILogger<WorkerService> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;

        var pollInterval = configuration.GetValue<int>("Worker:PollInterval");
        poll = pollInterval <= 0 ?  poll : TimeSpan.FromMilliseconds(pollInterval);
        workerIdentity = $"{Environment.MachineName}-{Environment.ProcessId}-{Guid.NewGuid()}";
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {                
                await using var scope = _serviceScopeFactory.CreateAsyncScope();
                var jobService = scope.ServiceProvider.GetRequiredService<IJobService>();
                var dispatcherService = scope.ServiceProvider.GetRequiredService<IDispatcherService>();

                var job = await jobService.AcquireJob(workerIdentity, cancellationToken);
                if (job != null)
                {
                    _logger.LogInformation("Worker: claimed job {JobId}", job.JobID);
                    var result = await dispatcherService.DispatchJob(job.Handler, job.Payload);
                    
                    //Do something with the result
                    continue;
                }

                _logger.LogInformation("Worker: no job found");
                await Task.Delay(poll, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Worker: {Message}", ex.Message);
                try
                {
                    await Task.Delay(poll, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
