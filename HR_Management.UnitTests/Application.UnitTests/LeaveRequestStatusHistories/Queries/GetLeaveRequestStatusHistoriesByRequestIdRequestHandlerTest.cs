using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Application.Features.LeaveRequestStatusHistories.Handlers.Queries;
using HR_Management.Application.Features.LeaveRequestStatusHistories.Requests.Queries;
using HR_Management.Common.Pagination;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveRequestStatusHistories.Queries;

public class GetLeaveRequestStatusHistoriesByRequestIdRequestHandlerTest : TestBase
{
    private readonly IMock<ILeaveRequestStatusHistoryRepository> _mockRepo;

    public GetLeaveRequestStatusHistoriesByRequestIdRequestHandlerTest()
    {
        _mockRepo = MockLeaveRequestStatusHistoryRepository.GetLeaveRequestStatusHistoriesByLeaveRequestIdMock();
    }

    [Fact]
    public async Task GetLeaveRequestStatusHistoriesByLeaveRequestIdTest()
    {
        //Arrange
        var handler = new GetLeaveRequestStatusHistoriesByRequestIdRequestHandler(_mockRepo.Object, _mapper);
        //Act
        var result = await handler.Handle(new GetLeaveRequestStatusHistoriesByRequestIdRequest
        {
            Pagination = new PaginationDto(),
            LeaveRequestId = 1
        }, CancellationToken.None);
        //Assert
        Assert.IsType<PagedResultDto<LeaveRequestStatusHistoryDto>>(result);
    }
}