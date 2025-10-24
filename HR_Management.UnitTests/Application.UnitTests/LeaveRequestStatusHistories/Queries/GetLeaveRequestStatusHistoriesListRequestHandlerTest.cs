using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Application.Features.LeaveRequestStatusHistories.Handlers.Queries;
using HR_Management.Application.Features.LeaveRequestStatusHistories.Requests.Queries;
using HR_Management.Common.Pagination;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveRequestStatusHistories.Queries;

public class GetLeaveRequestStatusHistoriesListRequestHandlerTest : TestBase
{
    private readonly Mock<ILeaveRequestStatusHistoryRepository> _mockRepo;

    public GetLeaveRequestStatusHistoriesListRequestHandlerTest()
    {
        _mockRepo = MockLeaveRequestStatusHistoryRepository.GetLeaveRequestStatusHistoriesListMock();
    }

    [Fact]
    public async Task GetLeaveRequestStatusHistoriesListTest()
    {
        //Arrange
        var handler = new GetLeaveRequestStatusHistoriesListRequestHandler(_mockRepo.Object, _mapper);
        //Act
        var result = await handler.Handle(new GetLeaveRequestStatusHistoriesListRequest
        {
            Pagination = new PaginationDto()
        }, CancellationToken.None);
        //Assert
        Assert.IsType<PagedResultDto<LeaveRequestStatusHistoryDto>>(result);
    }
}