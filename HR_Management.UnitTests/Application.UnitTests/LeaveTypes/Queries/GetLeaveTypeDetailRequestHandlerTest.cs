using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Queries;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.UnitTests.Mocks;
using Moq;
using static System.Int32;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypeDetailRequestHandlerTest : TestBase
{
    private readonly IMock<ILeaveTypeRepository> _mockRepo;

    public GetLeaveTypeDetailRequestHandlerTest()
    {
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypeDetailsMock();
    }

    [Theory]
    [InlineData(3, -1)]
    public async Task GetLeaveTypeDetailsTest(int validId, int inValidId)
    {
        //ValidMode
        //Arrange
        var handler = new GetLeaveTypeDetailRequestHandler(_mockRepo.Object, _mapper);

        //Act
        var validResult = await handler.Handle(new GetLeaveTypeDetailRequest
        {
            Id = validId
        }, CancellationToken.None);

        //Assert
        Assert.NotNull(validResult);
        Assert.InRange(validResult.Data.Id, 1, MaxValue);
        Assert.IsType<LeaveTypeDto>(validResult.Data);

        //InvalidMode
        //Act
        var invalidResult = await handler.Handle(new GetLeaveTypeDetailRequest
        {
            Id = inValidId
        }, CancellationToken.None);

        //Assert
        Assert.Null(invalidResult.Data);
        Assert.IsNotType<LeaveTypeDto>(invalidResult.Data);
    }
}