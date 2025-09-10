using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, ResultDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public DeleteLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
    }

    public async Task<ResultDto> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return ResultDto.Failure("Please Enter LeaveTypeId Correctly");

        var leaveType = await _leaveTypeRepo.Get(request.Id);
        if (leaveType == null) return ResultDto.Failure("نوع مرخصی مورد نظر پیدا نشد.");

        await _leaveTypeRepo.Delete(leaveType);
        return ResultDto.Success("نوع مرخصی مورد نظر با موفقیت حذف شد");
    }
}