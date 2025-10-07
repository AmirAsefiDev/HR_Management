using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Commands;

/// <summary>
///     This handler add leave allocation to all to users which doesn't have leave allocation with this leave type.
/// </summary>
public class LeaveTypeCreatedEvent : INotification
{
    public LeaveTypeCreatedEvent(int leaveTypeId, int defaultDays)
    {
        LeaveTypeId = leaveTypeId;
        DefaultDays = defaultDays;
    }

    public int LeaveTypeId { get; }
    public int DefaultDays { get; }
}