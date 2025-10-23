using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.Common;
using HR_Management.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveStatus.Commands;

public class CreateLeaveStatusCommandHandlerTest : TestBase
{
    private readonly CreateLeaveStatusDto _createLeaveStatusDto;
    private readonly IMock<ILeaveStatusRepository> _mockRepository;

    public CreateLeaveStatusCommandHandlerTest()
    {
        _mockRepository = MockLeaveStatusRepository.AddMock();
    }

    [Fact]
    public async Task Should_Create_LeaveStatus()
    {
        //Arrange
        var handler = new CreateLeaveStatusCommandHandler(_mockRepository.Object, _mapper);
        var dto = new CreateLeaveStatusDto
        {
            Name = "Vital",
            Description = "Simple Des"
        };
        //Act
        var result = await handler.Handle(new CreateLeaveStatusCommand
        {
            CreateLeaveStatusDto = dto
        }, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<ResultDto<int>>();
        var leaveStatus = await _mockRepository.Object.GetAllAsync();
        leaveStatus.Count.ShouldBe(4);
    }
}