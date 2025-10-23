using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveStatus.Commands;

public class DeleteLeaveStatusCommandHandlerTest : TestBase
{
    private readonly IMock<ILeaveRequestRepository> _mockRequestRepo;
    private readonly IMock<ILeaveRequestStatusHistoryRepository> _mockStatusHistoryRepo;
    private readonly IMock<ILeaveStatusRepository> _mockStatusRepo;

    public DeleteLeaveStatusCommandHandlerTest()
    {
        _mockStatusRepo = MockLeaveStatusRepository.DeleteMock();
        _mockRequestRepo = MockLeaveRequestRepository.HasAnyLeaveRequestWithStatusIdMock();
        _mockStatusHistoryRepo = MockLeaveRequestStatusHistoryRepository.HasAnyLeaveHistoryWithStatusIdAsync();
    }

    [Theory]
    [InlineData(3, 1, -1)]
    public async Task Should_Delete_LeaveStatus(int validId, int conflictId, int inValidId)
    {
        //Valid Mode
        //Arrange
        var handler = new DeleteLeaveStatusCommandHandler(_mockStatusRepo.Object, _mockRequestRepo.Object,
            _mockStatusHistoryRepo.Object);
        //Act
        var validResult = await handler.Handle(new DeleteLeaveStatusCommand
        {
            Id = validId
        }, CancellationToken.None);
        //Assert   
        Assert.True(validResult.IsSuccess);
        Assert.Equal(200, validResult.StatusCode);

        //ConflictMode
        //Act
        var conflictResult = await handler.Handle(new DeleteLeaveStatusCommand
        {
            Id = conflictId
        }, CancellationToken.None);

        //Assert
        Assert.False(conflictResult.IsSuccess);
        Assert.Equal(409, conflictResult.StatusCode);

        //InvalidMode
        //Act
        var invalidResult = await handler.Handle(new DeleteLeaveStatusCommand
        {
            Id = inValidId
        }, CancellationToken.None);
        //Assert
        Assert.False(invalidResult.IsSuccess);
        Assert.Equal(400, invalidResult.StatusCode);
    }
}