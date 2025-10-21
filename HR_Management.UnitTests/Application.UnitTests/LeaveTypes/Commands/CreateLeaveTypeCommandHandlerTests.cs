using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Commands;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Common;
using HR_Management.UnitTests.Common;
using HR_Management.UnitTests.Mocks;
using MediatR;
using Moq;
using Shouldly;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests : TestBase
{
    private readonly CreateLeaveTypeDto _createLeaveTypeDto;
    private readonly Mock<ILeaveTypeRepository> _mockRepository;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockRepository = MockLeaveTypeRepository.AddMock();

        _createLeaveTypeDto = new CreateLeaveTypeDto
        {
            DefaultDay = 99,
            Name = "Unique test"
        };
    }

    [Fact]
    public async Task Should_Create_LeaveType()
    {
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateLeaveTypeCommandHandler(_mockRepository.Object, _mapper, mockMediator.Object);
        var result = await handler.Handle(new CreateLeaveTypeCommand
        {
            LeaveTypeDto = _createLeaveTypeDto
        }, CancellationToken.None);

        result.ShouldBeOfType<ResultDto<int>>();

        var leaveTypes = await _mockRepository.Object.GetAllAsync();
        leaveTypes.Count.ShouldBe(4);
    }
}