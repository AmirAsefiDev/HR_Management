using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Handlers.Queries;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Common.Pagination;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveAllocations.Queries;

public class GetLeaveAllocationListRequestHandlerTest : TestBase
{
    private readonly Mock<ILeaveAllocationRepository> _mockRepo;

    public GetLeaveAllocationListRequestHandlerTest()
    {
        _mockRepo = MockLeaveAllocationRepository.GetLeaveAllocationsListMock();
    }

    [Fact]
    public async Task GetLeaveAllocationListTest()
    {
        //Arrange
        var handler = new GetLeaveAllocationListRequestHandler(_mockRepo.Object, _mapper);
        //Act
        var result = await handler.Handle(new GetLeaveAllocationListRequest
        {
            Pagination = new PaginationDto()
        }, CancellationToken.None);
        //Assert
        Assert.IsType<PagedResultDto<LeaveAllocationDto>>(result);
        Assert.Equal(3, result.Filter.First().Count);
    }
}