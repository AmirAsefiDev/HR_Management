using HR_Management.Application.DTOs.LeaveStatus;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Requests.Queries;

public class GetLeaveStatusListRequest : IRequest<List<LeaveStatusDto>>
{
}