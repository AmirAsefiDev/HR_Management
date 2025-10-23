using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using HR_Management.Common.Pagination;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveStatus.Queries;

public class GetLeaveStatusListRequestHandlerTest : TestBase
{
    private readonly Mock<ILeaveStatusRepository> _mockRepository;

    public GetLeaveStatusListRequestHandlerTest()
    {
        _mockRepository = MockLeaveStatusRepository.GetLeaveStatusesListMock();
    }

    [Fact]
    public async Task GetLeaveStatusListTest()
    {
        //Arrange
        var handler = new GetLeaveStatusListRequestHandler(_mockRepository.Object, _mapper);
        //Act
        var result = await handler.Handle(new GetLeaveStatusListRequest
            {
                Pagination = new PaginationDto()
            },
            CancellationToken.None);
        //Assert
        Assert.IsType<PagedResultDto<LeaveStatusDto>>(result);
    }
}