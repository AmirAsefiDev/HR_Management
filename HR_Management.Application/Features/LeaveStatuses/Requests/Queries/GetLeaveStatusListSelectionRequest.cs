using HR_Management.Application.DTOs.LeaveStatus;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Requests.Queries;

public class GetLeaveStatusListSelectionRequest : IRequest<List<LeaveStatusDto>>
{
}