using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveStatus.Queries;

public class GetLeaveStatusDetailRequestHandlerTest : TestBase
{
    private readonly Mock<ILeaveStatusRepository> _mockRepository;

    public GetLeaveStatusDetailRequestHandlerTest()
    {
        _mockRepository = MockLeaveStatusRepository.GetLeaveStatusDetailsMock();
    }

    [Theory]
    [InlineData(3, -3)]
    public async Task GetLeaveStatusDetailsTest(int validId, int inValidId)
    {
        //Valid mode
        //Arrange
        var handler = new GetLeaveStatusDetailRequestHandler(_mockRepository.Object, _mapper);
        //Act
        var result = await handler.Handle(new GetLeaveStatusDetailRequest
        {
            Id = validId
        }, CancellationToken.None);
        //Assert
        Assert.NotNull(result);
        Assert.InRange(result.Data.Id, 1, int.MaxValue);
        Assert.IsType<LeaveStatusDto>(result.Data);

        //Invalid mode
        //Act
        var invalidResult = await handler.Handle(new GetLeaveStatusDetailRequest
        {
            Id = inValidId
        }, CancellationToken.None);
        //Assert
        Assert.Null(invalidResult.Data);
        Assert.IsNotType<LeaveStatusDto>(invalidResult);
    }
}