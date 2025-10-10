using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HR_Management.Infrastructure.Job;

public class YearlyLeaveAllocationJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public YearlyLeaveAllocationJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            //Exactly at the first day of January.
            if (now is { Month: 1, Day: 1, Hour: 0 })
            {
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Send(new RebuildLeaveAllocationsForNewYearCommand(), stoppingToken);
            }

            //Every 24 hours check up again
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}