using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveTypes.Handlers.Commands;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveTypes.Commands;

public class DeleteLeaveTypeCommandHandlerTest : TestBase
{
    private readonly IMock<ILeaveAllocationRepository> _mockAllocationRepo;
    private readonly IMock<ILeaveRequestRepository> _mockRequestRepo;
    private readonly IMock<ILeaveTypeRepository> _mockTypeRepo;

    public DeleteLeaveTypeCommandHandlerTest()
    {
        _mockTypeRepo = MockLeaveTypeRepository.DeleteMock();
        _mockRequestRepo = MockLeaveRequestRepository.HasAnyLeaveRequestWithTypeIdMock();
        _mockAllocationRepo = MockLeaveAllocationRepository.HasAnyLeaveAllocationWithTypeIdMock();
    }

    [Theory]
    [InlineData(3, 1, -1)]
    public async Task Should_Delete_LeaveType(int validId, int conflictId, int inValidId)
    {
        //Valid Mode
        //Arrange
        var handler = new DeleteLeaveTypeCommandHandler(
            _mockTypeRepo.Object,
            _mockRequestRepo.Object,
            _mockAllocationRepo.Object);

        //Act
        var validResult = await handler.Handle(new DeleteLeaveTypeCommand
        {
            Id = validId
        }, CancellationToken.None);

        //Assert   
        Assert.True(validResult.IsSuccess);
        Assert.Equal(200, validResult.StatusCode);
        Assert.Equal("The requested leave type has been successfully deleted.", validResult.Message);


        //ConflictMode
        //Act
        var conflictResult = await handler.Handle(new DeleteLeaveTypeCommand
        {
            Id = conflictId
        }, CancellationToken.None);

        //Assert
        Assert.False(conflictResult.IsSuccess);
        Assert.Equal(409, conflictResult.StatusCode);


        //InvalidMode
        //Act
        var invalidResult = await handler.Handle(new DeleteLeaveTypeCommand
        {
            Id = inValidId
        }, CancellationToken.None);
        //Assert
        Assert.False(invalidResult.IsSuccess);
        Assert.Equal(400, invalidResult.StatusCode);
    }
}