using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class LeaveTypeCreatedEventHandler : INotificationHandler<LeaveTypeCreatedEvent>
{
    private readonly ILeaveManagementDbContext _context;

    public LeaveTypeCreatedEventHandler(ILeaveManagementDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LeaveTypeCreatedEvent notification, CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);
        foreach (var user in users)
        {
            var exists = await _context.LeaveAllocations.AnyAsync(la =>
                la.LeaveTypeId == notification.LeaveTypeId && la.UserId == user.Id, cancellationToken);

            if (!exists)
                await _context.LeaveAllocations.AddAsync(new LeaveAllocation
                {
                    UserId = user.Id,
                    LeaveTypeId = notification.LeaveTypeId,
                    TotalDays = notification.DefaultDays,
                    UsedDays = 0,
                    Period = DateTime.UtcNow.Year
                }, cancellationToken);

            await _context.SaveChangesAsync(true, cancellationToken);
        }
    }
}