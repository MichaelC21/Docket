using Docket.Persistence.Context;
using Docket.Worker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Docket.Worker.Services;

public class WorkerService : BackgroundService
{
    private TimeSpan poll = TimeSpan.FromMilliseconds(5000);
    private ILogger<WorkerService> _logger;    
    private IJobService _jobService;
    private IServiceScopeFactory _serviceScopeFactory;
    public WorkerService(ILogger<WorkerService> logger, IConfiguration configuration, IJobService jobService, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _jobService = jobService;
        _serviceScopeFactory = serviceScopeFactory;

        var pollInterval = configuration.GetValue<int>("Worker:PollInterval");
        poll = pollInterval <= 0 ?  poll : TimeSpan.FromMilliseconds(pollInterval);
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await using (var scope = _serviceScopeFactory.CreateAsyncScope())
            {
                _logger.LogInformation($"Worker: polling");
                try
                {           
                    var context = scope.ServiceProvider.GetRequiredService<DocketDBContext>();         
                    var job = await _jobService.AcquireJob(context, cancellationToken);

                    if(job != null)
                    {
                        _logger.LogInformation($"Acquired: {job.JobID.ToString()}");
                        //Process
                        continue;
                    }
                    
                    _logger.LogInformation("No job found");                    
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Worker: {ex.Message}");
                }              
                await Task.Delay(poll, cancellationToken); 
            }
        }
    }
}
