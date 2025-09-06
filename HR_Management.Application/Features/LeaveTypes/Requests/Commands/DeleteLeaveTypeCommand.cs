using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Commands;

public class DeleteLeaveTypeCommand : IRequest<ResultDto>
{
    public int Id { get; set; }
}