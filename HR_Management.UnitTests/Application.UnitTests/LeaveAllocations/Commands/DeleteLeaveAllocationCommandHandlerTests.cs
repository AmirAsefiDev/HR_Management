using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveAllocations.Commands;

public class DeleteLeaveAllocationCommandHandlerTests
{
    private readonly Mock<ILeaveAllocationRepository> _mockRepo;

    public DeleteLeaveAllocationCommandHandlerTests()
    {
        _mockRepo = MockLeaveAllocationRepository.DeleteMock();
    }

    [Theory]
    [InlineData(1, 5, -1)]
    public async Task Should_Delete_LeaveAllocation(int validId, int notExistId, int inValidId)
    {
        //Arrange
        var handler = new DeleteLeaveAllocationCommandHandler(_mockRepo.Object);
        //ValidMode
        //Act
        var validResult = await handler.Handle(new DeleteLeaveAllocationCommand
        {
            Id = validId
        }, CancellationToken.None);
        //Assert
        Assert.True(validResult.IsSuccess);
        Assert.Contains("successfully", validResult.Message);

        //NotFoundMode
        //Act
        var notFoundResult = await handler.Handle(new DeleteLeaveAllocationCommand
        {
            Id = notExistId
        }, CancellationToken.None);
        //Assert
        Assert.False(notFoundResult.IsSuccess);
        Assert.Contains("No leave allocation found", notFoundResult.Message);

        //InvalidMode
        //Act
        var inValidResult = await handler.Handle(new DeleteLeaveAllocationCommand
        {
            Id = inValidId
        }, CancellationToken.None);
        //Assert
        Assert.False(inValidResult.IsSuccess);
        Assert.Contains("enter", inValidResult.Message);
    }
}