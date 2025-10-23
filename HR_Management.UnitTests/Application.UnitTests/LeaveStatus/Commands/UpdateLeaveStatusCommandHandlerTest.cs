using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveStatus.Commands;

public class UpdateLeaveStatusCommandHandlerTest : TestBase
{
    private readonly IMock<ILeaveStatusRepository> _mockRepo;

    public UpdateLeaveStatusCommandHandlerTest()
    {
        _mockRepo = MockLeaveStatusRepository.UpdateMock();
    }

    [Fact]
    public async Task Should_Update_LeaveStatus()
    {
        //Arrange
        var handler = new UpdateLeaveStatusCommandHandler(_mockRepo.Object, _mapper);
        var dto = new UpdateLeaveStatusDto
        {
            Id = 3,
            Name = "Accepted",
            Description = "Simple accepted"
        };
        //Act
        var result = await handler.Handle(new UpdateLeaveStatusCommand
        {
            UpdateLeaveStatusDto = dto
        }, CancellationToken.None);
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.StatusCode);
    }
}