using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveStatus.Queries;

public class GetLeaveStatusListSelectionRequestHandlerTest : TestBase
{
    private readonly IMock<ILeaveStatusRepository> _mockRepo;

    public GetLeaveStatusListSelectionRequestHandlerTest()
    {
        _mockRepo = MockLeaveStatusRepository.GetLeaveStatusListSelection();
    }

    [Fact]
    public async Task GetLeaveStatusListSelectionTest()
    {
        //Arrange
        var handler = new GetLeaveStatusListSelectionRequestHandler(_mockRepo.Object, _mapper);
        //Act
        var result = await handler.Handle(new GetLeaveStatusListSelectionRequest(), CancellationToken.None);
        //Assert
        Assert.NotNull(result);
        Assert.IsType<List<LeaveStatusDto>>(result);
    }
}