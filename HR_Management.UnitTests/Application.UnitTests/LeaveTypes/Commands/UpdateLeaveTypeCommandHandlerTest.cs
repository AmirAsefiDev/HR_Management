using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Commands;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.UnitTests.Common;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveTypes.Commands;

public class UpdateLeaveTypeCommandHandlerTest : TestBase
{
    private readonly LeaveTypeDto _leaveTypeDto;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    public UpdateLeaveTypeCommandHandlerTest()
    {
        _leaveTypeDto = new LeaveTypeDto
        {
            Id = 3,
            DefaultDay = 30,
            Name = "Great"
        };
        _mockRepo = MockLeaveTypeRepository.UpdateMock();
    }

    [Fact]
    public async Task Should_Update_LeaveType()
    {
        //Arrange
        var handler = new UpdateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

        //Act
        var result = await handler.Handle(new UpdateLeaveTypeCommand
        {
            LeaveTypeDto = _leaveTypeDto
        }, CancellationToken.None);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("The requested leave type has been successfully updated.", result.Message);
        Assert.Equal(200, result.StatusCode);
    }
}